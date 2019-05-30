using System;
using System.Collections.Generic;
using System.Linq;
using HSourcer.Domain.Entities;
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
               new Team { Name = "Human Resources" , Description ="Human Resources.", OrganizationId = 1}
            };

            context.Teams.AddRange(teams);

            context.SaveChanges();
        }

        private void SeedUsers(HSourcerDbContext context)
        {
            User x = new User();
            var users = new[]{
                new User
                {
                    Active = true,
                    FirstName = "Jamie",
                    LastName = "Johanson",
                    Position = "Lead Designer",
                    PhoneNumber ="666 666 666",
                    Email= "jamie@companyname.com",
                    TeamId = 1,
                    PhotoPath = "text12345"
                },
                new User
                {
                    Active = true,
                    FirstName = "Agata",
                    LastName = "Smith",
                    Position = "Product Designer",
                    PhoneNumber ="666 123 666",
                    Email= "agata@companyname.com",
                    TeamId = 1,
                    PhotoPath = "text123"
                },
                new User
                {
                    Active = true,
                    FirstName = "Anna",
                    LastName = "Nowak",
                    Position = "Recruiter",
                    PhoneNumber ="666 123 666",
                    Email= "anna@companyname.com",
                    TeamId = 2,
                    PhotoPath = "text123"
                },
                new User
                {
                    Active = true,
                    FirstName = "Beatka",
                    LastName = "Nowak",
                    Position = "Tester",
                    PhoneNumber ="666 123 666",
                    Email= "beatka@companyname.com",
                    TeamId = 2,
                    PhotoPath = "text123xx"
                },
                new User
                {
                    Active = true,
                    FirstName = "Anna",
                    LastName = "Nowak",
                    Position = "Head of HR",
                    PhoneNumber ="666 123 666",
                    Email= "anna@companyname.com",
                    TeamId = 2,
                    PhotoPath = "text125",
                }
            };


            var hasher = new PasswordHasher<User>();
            foreach (var u in users)
            {
              u.PasswordHash =  hasher.HashPassword(u, "test123");
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
                    RoleId=1
                },
                new IdentityUserRole<int>
                {
                    UserId =4,
                    RoleId=2
                },
                new IdentityUserRole<int>
                {
                    UserId =5,
                    RoleId=2
                },
            };

            context.UserRoles.AddRange(usersRoles);
            context.SaveChanges();
        }

        private void SeedAbsences(HSourcerDbContext context)
        {
            var absences = new Absence[]
            {

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