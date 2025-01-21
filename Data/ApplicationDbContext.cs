using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspnetCoreStarter.Entities;
using AspnetCoreStarter.Entities.Articles;
using AspnetCoreStarter.Entities.Keywords;
using AspnetCoreStarter.Entities.Categories;
using AspnetCoreStarter.Entities.Posts;

namespace AspnetCoreStarter.Data
{
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //    : base(options)
    //{
    //}

    public DbSet<User> User { get; set; } = default!;
    public DbSet<Article> Articles { get; set; } = default!;
    public DbSet<Language> Languages { get; set; } = default!;
    public DbSet<Entities.Posts.Attribute> Attributes { get; set; } = default!;
    public DbSet<PostType> PostTypes { get; set; } = default!;
    public DbSet<Keyword> Keywords { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      //OBS!: you can filter types within the assembly based on context name, usefull on multitenant solutions
      builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
  }
}
