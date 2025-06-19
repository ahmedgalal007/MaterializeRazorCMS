using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities;
using EFCore.AutomaticMigrations;
using Microsoft.EntityFrameworkCore;
using AspnetCoreStarter.Services.Interfaces;
using AspnetCoreStarter.Services;

namespace AspnetCoreStarter.Extenssions;

public static class AppSettingsExtensions
{
  public static async Task StartSettingsService(this IHost host)
  {
    await using var scope = host.Services.CreateAsyncScope();
    var services = scope.ServiceProvider;
    await using ApplicationSettingsDbContext? dbContext = services.GetService<ApplicationSettingsDbContext>();
    if (dbContext is not null)
    {
      //if (await dbContext.Database.EnsureCreatedAsync())
      //{
      //  string q1 = "CREATE TABLE \"__EFMigrationsHistory\" ( ";
      //  string q2 = "              \"MigrationId\" TEXT NOT NULL CONSTRAINT \"PK___EFMigrationsHistory\" PRIMARY KEY, ";
      //  string q3 = "              \"ProductVersion\" TEXT NOT NULL";
      //  string q4 = "); ";
      //  string q5 = "INSERT OR IGNORE INTO \"__EFMigrationsHistory\" ";
      //  string q6 = "       (\"MigrationId\", \"ProductVersion\") ";
      //  string q7 = $"VALUES ('{DateTime.Now.ToString("yyyyMMddHHmmss")}_InitialCreate', '1.0.0');";
      //  dbContext.Database.ExecuteSql($"CREATE TABLE \"__EFMigrationsHistory\" (  \"MigrationId\" TEXT NOT NULL CONSTRAINT \"PK___EFMigrationsHistory\" PRIMARY KEY, \"ProductVersion\" TEXT NOT NULL); INSERT INTO \"__EFMigrationsHistory\" (\"MigrationId\", \"ProductVersion\") VALUES ('{DateTime.Now.ToString("yyyyMMddHHmmss")}_InitialCreate', '1.0.0');");
      //}

      // Create Database and Applly Migrations
      // await MigrateDatabaseToLatestVersion.ExecuteAsync(dbContext);
      await dbContext.MigrateToLatestVersionAsync(new DbMigrationsOptions { AutomaticMigrationsEnabled = true, ResetDatabaseSchema = false, AutomaticMigrationDataLossAllowed = true });

      //var Migrator = new MigrateDatabaseToLatestVersion<ApplicationDbContext, ApplicationDbContextConfiguration>();
      //Migrator.InitializeDatabase(dbContext);

      // Seed Data
      // SeedData.Initialize(services);
      await SeedTablesConfig.Initialize(services);
      // Seed initial settings if needed
      if (!dbContext.AppSettings.Any())
      {
        dbContext.AppSettings.Add(new AppSetting { Key = "WelcomeMessage", Value = "Hello from Database!", LastModified = DateTime.UtcNow });
        dbContext.AppSettings.Add(new AppSetting { Key = "MaxUsers", Value = "100", LastModified = DateTime.UtcNow });
        dbContext.SaveChanges();
      }

       ISettingService? appSettings = services.GetService<ISettingService>();
      if (appSettings is not null) {
        var st = await appSettings.GetAllSettingsAsync();
      }
    }

    
  }
}


//internal sealed class ApplicationDbContextConfiguration :
//    DbMigrationsConfiguration<ApplicationDbContext>
//{
//  public ApplicationDbContextConfiguration()
//  {
//    AutomaticMigrationsEnabled = true;
//  }
//}


// if (!await dbContext.Database.EnsureCreatedAsync())
//{
//  dbContext.Database..ExecuteSqlCommand(@"
//        CREATE TABLE IF NOT EXISTS ""__EFMigrationsHistory"" (
//            ""MigrationId"" TEXT NOT NULL CONSTRAINT ""PK___EFMigrationsHistory"" PRIMARY KEY,
//            ""ProductVersion"" TEXT NOT NULL
//        );

//        INSERT OR IGNORE INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
//        VALUES ('20160627142134_InitialCreate', '1.0.0');        
//    ");
//   }
