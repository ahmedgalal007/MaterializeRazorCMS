using AspnetCoreStarter.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreStarter.Data;


public class ApplicationSettingsDbContext : DbContext
{
  public ApplicationSettingsDbContext(DbContextOptions<ApplicationSettingsDbContext> options)
      : base(options)
  {
  }

  public DbSet<AppSetting> AppSettings { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Optional: Add unique constraint to Key to prevent duplicate setting names
    modelBuilder.Entity<AppSetting>()
        .HasIndex(s => s.Key)
        .IsUnique();

    base.OnModelCreating(modelBuilder);
  }
}
