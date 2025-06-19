using AspnetCoreStarter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AspnetCoreStarter.Data;


public class ApplicationSettingsDbContext(DbContextOptions<ApplicationSettingsDbContext> options)
      : DbContext(options)
{


  public DbSet<AppSetting> AppSettings { get; set; } = null!;
  public DbSet<DynamicTableConfig> TablesConfig { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Optional: Add unique constraint to Key to prevent duplicate setting names
    modelBuilder.Entity<AppSetting>()
        .HasIndex(s => s.Key)
        .IsUnique();


    base.OnModelCreating(modelBuilder);
  }
}
