using System;
using System.Collections.Generic;
using System.Linq;
using HSourcer.Domain.Entities;
using HSourcer.Domain.Enums;
using HSourcer.Domain.Security;
using Microsoft.AspNetCore.Identity;

namespace HSourcer.Persistence
{
    public class HSourcerInitializer
    {
        public static void Initialize(HSourcerDbContext context)
        {
            var initializer = new HSourcerInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(HSourcerDbContext context)
        {
            context.Database.EnsureCreated();

            SeedRoles(context);

            SeedOrganizations(context);
            SeedTeams(context);
            SeedUsers(context);
            SeedAbsences(context);
        }

        private void SeedRoles(HSourcerDbContext context)
        {
            var roles = new[]
            {
               new IdentityRole<int> {Name="TEAM_LEADER" , NormalizedName="TEAM_LEADER"},
               new IdentityRole<int> {Name="EMPLOYEE", NormalizedName="EMPLOYEE"},
               new IdentityRole<int> {Name="ADMIN", NormalizedName="ADMIN"}
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        private void SeedOrganizations(HSourcerDbContext context)
        {
            var organizations = new[]
            {
               new Organization {Name = "FirstOrganization" , Description ="Seeded organization."}
            };

            context.Organizations.AddRange(organizations);

            context.SaveChanges();
        }
        private void SeedTeams(HSourcerDbContext context)
        {
            var teams = new[]
            {
               new Team { Name = "Design" , Description ="Design.", OrganizationId = 1},
               new Team { Name = "Human Resources" , Description ="Human Resources.", OrganizationId = 1},
               new Team { Name = "Development" , Description ="Development.", OrganizationId = 1},
               new Team { Name = "Management" , Description ="Management.", OrganizationId = 1}
            };

            context.Teams.AddRange(teams);

            context.SaveChanges();
        }

        private void SeedUsers(HSourcerDbContext context)
        {
            var users = new[]{
                new User
                {
                    Active = true,
                    FirstName = "Jamie",
                    LastName = "Johanson",
                    Position = "Lead Designer",
                    PhoneNumber = "5555",
                    Email= "designerteamleader@hscr.site",
                    TeamId = 1,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/f/1.jpg?fbclid=IwAR3ArRnVKAanZwYn0SqYbVM3aLr1vy71U89uBYpUX06YUunPu6CK3_zZijw",
                    UserRole= "TEAM_LEADER"

                },
                new User
                {
                    Active = true,
                    FirstName = "Jamie",
                    LastName = "Stevenson",
                    PhoneNumber = "5555",
                    Position = "UX Designer",
                    Email= "uxdesigner@hscr.site",
                    TeamId = 1,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/f/1.jpg?fbclid=IwAR3ArRnVKAanZwYn0SqYbVM3aLr1vy71U89uBYpUX06YUunPu6CK3_zZijw",
                    UserRole= "EMPLOYEE"

                },
                new User
                {
                    Active = true,
                    FirstName = "Jamie",
                    LastName = "Potter",
                    PhoneNumber = "5555",
                    Position = "UI Designer",
                    Email= "uidesigner@hscr.site",
                    TeamId = 1,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/f/1.jpg?fbclid=IwAR3ArRnVKAanZwYn0SqYbVM3aLr1vy71U89uBYpUX06YUunPu6CK3_zZijw",
                    UserRole= "EMPLOYEE"

                },
                new User
                {
                    Active = true,
                    FirstName = "Tomasz",
                    LastName = "L",
                    PhoneNumber = "5555",
                    Position = "Lord Admin",
                    Email= "admin@hscr.site",
                    TeamId = 3,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/f/1.jpg?fbclid=IwAR3ArRnVKAanZwYn0SqYbVM3aLr1vy71U89uBYpUX06YUunPu6CK3_zZijw",
                    UserRole= "ADMIN"

                },
                new User
                {
                    Active = true,
                    FirstName = "Agata",
                    LastName = "Smith",
                    PhoneNumber = "5555",
                    Position = "Management Leader",
                    Email= "ceo@hscr.site",
                    TeamId = 4,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/m/1.jpg?fbclid=IwAR0kNsCM7JKA-gJbsjM_9jr9SS9nSF5M1rmSdNx4uLMAbhr8HsZpMrOAovk",
                    UserRole= "TEAM_LEADER"
                },
                new User
                {
                    Active = true,
                    FirstName = "Agata",
                    LastName = "Nowak",
                    PhoneNumber = "5555",
                    Position = "Management",
                    Email= "cso@hscr.site",
                    TeamId = 4,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/m/1.jpg?fbclid=IwAR0kNsCM7JKA-gJbsjM_9jr9SS9nSF5M1rmSdNx4uLMAbhr8HsZpMrOAovk",
                    UserRole= "EMPLOYEE"
                },
                new User
                {
                    Active = true,
                    FirstName = "Agata",
                    LastName = "Ptak",
                    PhoneNumber = "5555",
                    Position = "Management",
                    Email= "cto@hscr.site",
                    TeamId = 4,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/m/1.jpg?fbclid=IwAR0kNsCM7JKA-gJbsjM_9jr9SS9nSF5M1rmSdNx4uLMAbhr8HsZpMrOAovk",
                    UserRole= "EMPLOYEE"
                },
                new User
                {
                    Active = true,
                    FirstName = "Stefan",
                    LastName = "Moonwalk",
                    PhoneNumber = "5555",
                    Position = "Development lead",
                    Email= "developmentteamleader@hscr.site",
                    TeamId = 3,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/m/1.jpg?fbclid=IwAR0kNsCM7JKA-gJbsjM_9jr9SS9nSF5M1rmSdNx4uLMAbhr8HsZpMrOAovk",
                    UserRole= "TEAM_LEAD"
                },
                new User
                {
                    Active = true,
                    FirstName = "Paweł",
                    LastName = "Moonwalk",
                    PhoneNumber = "5555",
                    Position = "Developer",
                    Email= "developer@hscr.site",
                    TeamId = 3,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/m/1.jpg?fbclid=IwAR0kNsCM7JKA-gJbsjM_9jr9SS9nSF5M1rmSdNx4uLMAbhr8HsZpMrOAovk",
                    UserRole= "EMPLOYEE"
                },
                new User
                {
                    Active = true,
                    FirstName = "Sebastion",
                    LastName = "Moonwalk",
                    PhoneNumber = "5555",
                    Position = "Dev Ops",
                    Email= "devops@hscr.site",
                    TeamId = 3,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/m/1.jpg?fbclid=IwAR0kNsCM7JKA-gJbsjM_9jr9SS9nSF5M1rmSdNx4uLMAbhr8HsZpMrOAovk",
                    UserRole= "EMPLOYEE"
                },
                new User
                {
                    Active = true,
                    FirstName = "Agata",
                    LastName = "Tragedy",
                    PhoneNumber = "5555",
                    Position = "HR Team Leader",
                    Email= "hrteamleader@hscr.site",
                    TeamId = 2,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/m/1.jpg?fbclid=IwAR0kNsCM7JKA-gJbsjM_9jr9SS9nSF5M1rmSdNx4uLMAbhr8HsZpMrOAovk",
                    UserRole= "TEAM_LEADER"
                },
                new User
                {
                    Active = true,
                    FirstName = "Agata",
                    LastName = "Smith",
                    Position = "Recruiter",
                    Email= "recruiter@hscr.site",
                    TeamId = 2,
                    PhotoPath = "https://hsourcerapp.azurewebsites.net/images/mock/m/1.jpg?fbclid=IwAR0kNsCM7JKA-gJbsjM_9jr9SS9nSF5M1rmSdNx4uLMAbhr8HsZpMrOAovk",
                    UserRole= "EMPLOYEE"
                }

            };
            foreach(var u in users)
            {
                u.NormalizedEmail = u.Email.ToUpper();
                u.UserName = u.Email;
            }

            var hasher = new PasswordHasher<User>();
            foreach (var u in users)
            {
                u.PasswordHash = hasher.HashPassword(u, "Test12#$");
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            var usersRoles = new[]
            {
                new IdentityUserRole<int>
                {
                    UserId =1,
                    RoleId=1
                },

                new IdentityUserRole<int>
                {
                    UserId =2,
                    RoleId=2
                },
                new IdentityUserRole<int>
                {
                    UserId =3,
                    RoleId=2
                },
                new IdentityUserRole<int>
                {
                    UserId =4,
                    RoleId=3
                },
                new IdentityUserRole<int>
                {
                    UserId =5,
                    RoleId=1
                },
                new IdentityUserRole<int>
                {
                    UserId =6,
                    RoleId=2
                },
                new IdentityUserRole<int>
                {
                    UserId =7,
                    RoleId=2
                },
                new IdentityUserRole<int>
                {
                    UserId =8,
                    RoleId=1
                },
                new IdentityUserRole<int>
                {
                    UserId =9,
                    RoleId=2
                },
                new IdentityUserRole<int>
                {
                    UserId =10,
                    RoleId=2
                },
                new IdentityUserRole<int>
                {
                    UserId =11,
                    RoleId=1
                },
                new IdentityUserRole<int>
                {
                    UserId =12,
                    RoleId=2
                },

            };

            context.UserRoles.AddRange(usersRoles);
            context.SaveChanges();
        }

        private void SeedAbsences(HSourcerDbContext context)
        {
            var absences = new []
            {
                new Absence { AbsenceId = 1, UserId = 1, Status = Convert.ToInt32(StatusEnum.PENDING), Comment ="Design."},
            };

            context.Absences.AddRange(absences);

            context.SaveChanges();
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}