
using AspnetCoreStarter.Data;

namespace AspnetCoreStarter.Auth;

public class AuthorizationManager : IAuthorizationManager
{
  private readonly ApplicationDbContext dbContext;

  public AuthorizationManager()
  {
  }

  public AuthorizationManager(ApplicationDbContext dbContext)
  {
    this.dbContext = dbContext;
  }
  public IEnumerable<String> Actions => new List<string>();

  public IEnumerable<String> Resources => new List<string>();

  public IEnumerable<String> ResourceActions => new List<string>();

  public IEnumerable<String> Roles => new List<string>();

  public IEnumerable<String> Permissions => new List<string>();

  public IEnumerable<String> RolePermissions => new List<string>();
}
