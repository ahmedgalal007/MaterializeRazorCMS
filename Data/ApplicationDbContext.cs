using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspnetCoreStarter.Entities;
using AspnetCoreStarter.Entities.Articles;
using AspnetCoreStarter.Entities.Keywords;
using AspnetCoreStarter.Entities.Categories;

namespace AspnetCoreStarter.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> User { get; set; } = default!;
    public DbSet<Article> Articles { get; set; } = default!;
    public DbSet<Language> Languages { get; set; } = default!;
    public DbSet<Entities.Posts.Attribute> Attributes { get; set; } = default!;
    public DbSet<Keyword> Keywords { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
  }
}
