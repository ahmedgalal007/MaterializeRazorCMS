using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AspnetCoreStarter.Entities;
using Attribute = AspnetCoreStarter.Entities.Posts.Attribute;
using AspnetCoreStarter.Common;

namespace AspnetCoreStarter.Data
{
  public static class SeedData
  {
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var configDb = new ApplicationSettingsDbContext(
          serviceProvider.GetRequiredService<DbContextOptions<ApplicationSettingsDbContext>>()))
      {

        using (var context = new ApplicationDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>(), configDb))
        {


          // Check if there is already data in the database
          if (context.User.Any() && context.Attributes.Any())
          {
            return; // Database has been seeded
          }

          if (!context.User.Any())
          {
            // Seed some sample data
            var users = new User[]
            {
              new User
              {
                // Id = 1,
                UserName = "John Doe",
                Email = "john.doe@example.com",
                IsVerified = true,
                ContactNumber = "+1 (123) 456-7890",
                SelectedRole = "admin",
                AvailableRoles = new List<string> { "subscriber", "editor", "maintainer", "author", "admin" },
                SelectedPlan = "enterprise",
                AvailablePlans = new List<string> { "basic", "enterprise", "company", "team" }
              },
              new User
              {
                // Id = 2,
                UserName = "Jane Smith",
                Email = "jane.smith@example.com",
                IsVerified = false,
                ContactNumber = "+1 (987) 654-3210",
                SelectedRole = "author",
                AvailableRoles = new List<string> { "subscriber", "editor", "maintainer", "author", "admin" },
                SelectedPlan = "company",
                AvailablePlans = new List<string> { "basic", "enterprise", "company", "team" }
              },
              new User
              {
                // Id = 3,
                UserName = "Alice Johnson",
                Email = "alice.johnson@example.com",
                IsVerified = true,
                ContactNumber = "+1 (555) 555-5555",
                SelectedRole = "editor",
                AvailableRoles = new List<string> { "subscriber", "editor", "maintainer", "author", "admin" },
                SelectedPlan = "basic",
                AvailablePlans = new List<string> { "basic", "enterprise", "company", "team" }
              },
              new User
              {
                // Id = 4,
                UserName = "Bob Wilson",
                Email = "bob.wilson@example.com",
                IsVerified = false,
                ContactNumber = "+1 (777) 777-7777",
                SelectedRole = "subscriber",
                AvailableRoles = new List<string> { "subscriber", "editor", "maintainer", "author", "admin" },
                SelectedPlan = "team",
                AvailablePlans = new List<string> { "basic", "enterprise", "company", "team" }
              },
              new User
              {
                // Id = 5,
                UserName = "Eva Brown",
                Email = "eva.brown@example.com",
                IsVerified = true,
                ContactNumber = "+1 (999) 999-9999",
                SelectedRole = "author",
                AvailableRoles = new List<string> { "subscriber", "editor", "maintainer", "author", "admin" },
                SelectedPlan = "basic",
                AvailablePlans = new List<string> { "basic", "enterprise", "company", "team" }
              },
              // Add more user data as needed below...
            };

            context.User.AddRange(users);
            context.SaveChanges();
          }

          if (!context.Attributes.Any())
          {
            var attributes = new Attribute[] {
              new Attribute() { Name = "Text", BaseType = typeof(string).FullName ?? typeof(string).Name, Description = "Simple Text Value", ReturnType = typeof(string).Name },
              new Attribute() { Name = "Number", BaseType = typeof(double).FullName ?? typeof(double).Name, Description = "Simple Number Value", ReturnType = typeof(double).Name },
              new Attribute() { Name = "Decimal", BaseType = typeof(decimal).FullName ?? typeof(decimal).Name, Description = "Simple Decimal Field", ReturnType = typeof(decimal).Name }
            };

            context.Attributes.AddRange(attributes);
            context.SaveChanges();
          }

          if (!context.Languages.Any())
          {
            foreach (var item in ISOLanguages.languages)
            {
              context.Languages.Add(item);
            }

            context.SaveChanges();
          }

        }
      }
    }
  }
}
