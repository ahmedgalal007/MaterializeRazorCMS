namespace AspnetCoreStarter.Auth;

public interface IAuthorizationManager
{
  IEnumerable<string> Actions { get; }
  IEnumerable<string> Resources { get; }
  IEnumerable<string> ResourceActions { get; }
  IEnumerable<string> Roles { get; }
  IEnumerable<string> Permissions { get; }
  IEnumerable<string> RolePermissions { get; }

}
