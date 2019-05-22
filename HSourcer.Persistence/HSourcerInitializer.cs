using System;
using System.Collections.Generic;
using System.Linq;
using HSourcer.Domain.Entities;

namespace HSourcer.Persistence
{
    public class HSourcerInitializer
    {
        //to be used later maybe
        private readonly Dictionary<int, User> Users = new Dictionary<int, User>();

        public static void Initialize(HSourcerDbContext context)
        {
            var initializer = new HSourcerInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(HSourcerDbContext context)
        {
            context.Database.EnsureCreated();


            SeedOrganizations(context);
            SeedTeams(context);
            SeedUsers(context);
            SeedAbsences(context);
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
            var users = new[]{
                new User
                {
                Active = true,
                FirstName = "Jamie",
                LastName = "Johanson",
                Position = "Lead Designer",
                PhoneNumber ="666 666 666",
                EmailAddress= "jamie@companyname.com",
                TeamId = 1,
                PhotoPath = "text12345",
                Role = 1
                },
                new User
                {
                Active = true,
                FirstName = "Agata",
                LastName = "Smith",
                Position = "Product Designer",
                PhoneNumber ="666 123 666",
                EmailAddress= "agata@companyname.com",
                TeamId = 1,
                PhotoPath = "text123",
                Role = 2
                },
                new User
                {
                Active = true,
                FirstName = "Anna",
                LastName = "Nowak",
                Position = "Recruiter",
                PhoneNumber ="666 123 666",
                EmailAddress= "anna@companyname.com",
                TeamId = 2,
                PhotoPath = "text123",
                Role = 1
                },
                new User
                {
                Active = true,
                FirstName = "Beatka",
                LastName = "Nowak",
                Position = "Tester",
                PhoneNumber ="666 123 666",
                EmailAddress= "beatka@companyname.com",
                TeamId = 2,
                PhotoPath = "text123xx",
                Role = 2
                },
                new User
                {
                Active = true,
                FirstName = "Anna",
                LastName = "Nowak",
                Position = "Head of HR",
                PhoneNumber ="666 123 666",
                EmailAddress= "anna@companyname.com",
                TeamId = 2,
                PhotoPath = "text125",
                Role = 2
                }
            };
            context.AddRange(users);
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