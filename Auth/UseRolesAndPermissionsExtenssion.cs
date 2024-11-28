using AspnetCoreStarter.Data;

namespace AspnetCoreStarter.Auth;

public static class UseRolesAndPermissionsExtenssion
{
  /// <summary>
  /// Initialize the roles and permissions from database
  /// It must be added after UseAuthorizations Extenssion
  /// </summary>
  /// <param name="app"></param>
  /// <returns></returns>
  public static async Task<IApplicationBuilder> UseRolesAndPermissions(this IApplicationBuilder app) {
    using (var scope = app.ApplicationServices.CreateAsyncScope())
    {
      ApplicationDbContext? db = scope.ServiceProvider.GetService<ApplicationDbContext>();
      if (db == null) { return app; }
      var ResultSet = db.User.ToList();
    }
    return app;
  }
}
