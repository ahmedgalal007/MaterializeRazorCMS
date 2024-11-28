using System.Collections.ObjectModel;

namespace AspnetCoreStarter.Auth.Authorization
{
  public static class ApplicationActions
  {
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
  }

  public static class ApplicationResources
  {
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
    public const string Categories = nameof(Categories);
    public const string Keywords = nameof(Keywords);
    public const string News = nameof(News);

  }

  public static class ApplicationPermissions
  {
    private static readonly ApplicationPermission[] _all = new ApplicationPermission[]
    {
        new("View Dashboard", ApplicationActions.View, ApplicationResources.Dashboard),
        new("View Hangfire", ApplicationActions.View, ApplicationResources.Hangfire),
        new("View Users", ApplicationActions.View, ApplicationResources.Users),
        new("Search Users", ApplicationActions.Search, ApplicationResources.Users),
        new("Create Users", ApplicationActions.Create, ApplicationResources.Users),
        new("Update Users", ApplicationActions.Update, ApplicationResources.Users),
        new("Delete Users", ApplicationActions.Delete, ApplicationResources.Users),
        new("Export Users", ApplicationActions.Export, ApplicationResources.Users),
        new("View UserRoles", ApplicationActions.View, ApplicationResources.UserRoles),
        new("Update UserRoles", ApplicationActions.Update, ApplicationResources.UserRoles),
        new("View Roles", ApplicationActions.View, ApplicationResources.Roles),
        new("Create Roles", ApplicationActions.Create, ApplicationResources.Roles),
        new("Update Roles", ApplicationActions.Update, ApplicationResources.Roles),
        new("Delete Roles", ApplicationActions.Delete, ApplicationResources.Roles),
        new("View RoleClaims", ApplicationActions.View, ApplicationResources.RoleClaims),
        new("Update RoleClaims", ApplicationActions.Update, ApplicationResources.RoleClaims),
        new("View Products", ApplicationActions.View, ApplicationResources.Products, IsBasic: true),
        new("Search Products", ApplicationActions.Search, ApplicationResources.Products, IsBasic: true),
        new("Create Products", ApplicationActions.Create, ApplicationResources.Products),
        new("Update Products", ApplicationActions.Update, ApplicationResources.Products),
        new("Delete Products", ApplicationActions.Delete, ApplicationResources.Products),
        new("Export Products", ApplicationActions.Export, ApplicationResources.Products),
        new("View Brands", ApplicationActions.View, ApplicationResources.Brands, IsBasic: true),
        new("Search Brands", ApplicationActions.Search, ApplicationResources.Brands, IsBasic: true),
        new("Create Brands", ApplicationActions.Create, ApplicationResources.Brands),
        new("Update Brands", ApplicationActions.Update, ApplicationResources.Brands),
        new("Delete Brands", ApplicationActions.Delete, ApplicationResources.Brands),
        new("Generate Brands", ApplicationActions.Generate, ApplicationResources.Brands),
        new("Clean Brands", ApplicationActions.Clean, ApplicationResources.Brands),
        new("View Tenants", ApplicationActions.View, ApplicationResources.Tenants, IsRoot: true),
        new("Create Tenants", ApplicationActions.Create, ApplicationResources.Tenants, IsRoot: true),
        new("Update Tenants", ApplicationActions.Update, ApplicationResources.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", ApplicationActions.UpgradeSubscription, ApplicationResources.Tenants, IsRoot: true),
        new("View News", ApplicationActions.View, ApplicationResources.News, IsBasic: true),
        new("Search News", ApplicationActions.Search, ApplicationResources.News, IsBasic: true),
        new("Create News", ApplicationActions.Create, ApplicationResources.News),
        new("Update News", ApplicationActions.Update, ApplicationResources.News),
        new("Delete News", ApplicationActions.Delete, ApplicationResources.News),
        new("Export News", ApplicationActions.Export, ApplicationResources.News),
        new("View Categories", ApplicationActions.View, ApplicationResources.Categories, IsBasic: true),
        new("Search Categories", ApplicationActions.Search, ApplicationResources.Categories, IsBasic: true),
        new("Create Categories", ApplicationActions.Create, ApplicationResources.Categories),
        new("Update Categories", ApplicationActions.Update, ApplicationResources.Categories),
        new("Delete Categories", ApplicationActions.Delete, ApplicationResources.Categories),
        new("Generate Categories", ApplicationActions.Generate, ApplicationResources.Categories),
        new("Clean Categories", ApplicationActions.Clean, ApplicationResources.Categories),
        new("View Keywords", ApplicationActions.View, ApplicationResources.Keywords, IsBasic: true),
        new("Search Keywords", ApplicationActions.Search, ApplicationResources.Keywords, IsBasic: true),
        new("Create Keywords", ApplicationActions.Create, ApplicationResources.Keywords),
        new("Update Keywords", ApplicationActions.Update, ApplicationResources.Keywords),
        new("Delete Keywords", ApplicationActions.Delete, ApplicationResources.Keywords),
    };

    public static IReadOnlyList<ApplicationPermission> All { get; } = new ReadOnlyCollection<ApplicationPermission>(_all);
    public static IReadOnlyList<ApplicationPermission> Root { get; } = new ReadOnlyCollection<ApplicationPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<ApplicationPermission> Admin { get; } = new ReadOnlyCollection<ApplicationPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<ApplicationPermission> Basic { get; } = new ReadOnlyCollection<ApplicationPermission>(_all.Where(p => p.IsBasic).ToArray());
  }

  public record ApplicationPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
  {
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
  }
}
