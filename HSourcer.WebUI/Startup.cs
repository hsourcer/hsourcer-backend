using AutoMapper;
using HSourcer.Application.Infrastructure;
using HSourcer.Application.Infrastructure.AutoMapper;
using HSourcer.Application.Interfaces;
using HSourcer.Application.Users.Commands;
using HSourcer.Common;
using HSourcer.Domain.Entities;
using HSourcer.Domain.Security;
using HSourcer.Infrastructure;
using HSourcer.Persistence;
using HSourcer.WebUI.Auth;
using HSourcer.WebUI.Filters;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace HSourcer.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            #endregion

            #region Framework services
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDateTime, MachineDateTime>();
            #endregion

            #region Add MediatR
            services.AddMediatR();
            services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);
             #endregion

            #region Service Behaviour
            #region Logging for performance
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            #endregion
            #region Request validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            #endregion
            #endregion
         
            #region return 404 instead of incorrect model info
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion
        
            #region DbContext
            services.AddDbContext<IHSourcerDbContext, HSourcerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HSourcerDatabase")));
            #endregion

            #region Add entity + context
            //for .net identity so the DBContext can be resolved,
            //we have only one context so it should be fine
            //not sure if need to be scoped
            services.AddScoped<DbContext, HSourcerDbContext>();

            services.AddIdentity<User, IdentityRole<int>>()
               .AddRoles<IdentityRole<int>>()
               .AddEntityFrameworkStores<HSourcerDbContext>()
               .AddDefaultTokenProviders();

            //not sure if it need to be scoped
            services.AddScoped<IUserStore<User>, Persistence.UserStore>();

            services.AddScoped<IRoleStore<IdentityRole<int>>, RoleStore>();

            #endregion


            #region MVC
            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #endregion

            #region Authorization with .net core identity
            #region Configure password and standard settings.
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            #endregion
            #region Configure token
            services.AddAuthentication
                (JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["TokenConfig:Issuer"],
                            ValidAudience = Configuration["TokenConfig:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey
                                (Encoding.UTF8.GetBytes(Configuration["TokenConfig:Key"]))
                        };
                    });
            #endregion
            #region Add TokenConfig
            //we will pass only token config
            services.Configure<TokenConfig>(Configuration.GetSection("TokenConfig"));
            #endregion
            #region Add service for getting user Identity
            services.AddTransient<UserResolverService>();
            #endregion
            #endregion

            #region API versioning
            //https://gingter.org/2018/06/18/asp-net-core-api-versioning/
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options); // optional, but recommended
            });
            #endregion

            #region Static files
            //TODO check how to publis react so it can utilize it...
            services.AddSpaStaticFiles(configuration => configuration.RootPath = $"ClientApp/dist");
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "HSourcer API",
                    Description = "HSourcere API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "HSourcer Team", Email = "HSourcerTeam@gmail.com", Url = "not url for now" }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Errors redirection
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            #endregion

            #region Standard uses
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            #endregion

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HSourcer API");
            });
            #endregion

            #region Auth
            app.UseAuthentication();
            #endregion

            #region MVC Routes
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{version}/{controller}/{action=Index}/{id?}");
            });
            #endregion

            #region SPA
            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "ClientApp";
                    spa.UseReactDevelopmentServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });
            #endregion
        }
    }
}

