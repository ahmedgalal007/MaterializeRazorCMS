using Microsoft.AspNetCore.Mvc;
using AspnetCoreStarter.Entities;
using AspnetCoreStarter.Data;
using System.Xml.Linq;
using AspnetCoreStarter.Endpoints.Users;

namespace AspnetCoreStarter.Endpoints
{
  public static class ApiEndpointsExtension
  {
    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
      new UsersEndpoints(routes, "/api/User", nameof(User));
    }
  }
}
