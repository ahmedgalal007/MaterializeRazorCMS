using Microsoft.AspNetCore.Mvc;
using AspnetCoreStarter.Entities;
using AspnetCoreStarter.Data;
using System.Xml.Linq;
using AspnetCoreStarter.Endpoints.Users;
using AspnetCoreStarter.Entities.Posts;
using AspnetCoreStarter.Endpoints.Schemas;
using AspnetCoreStarter.Endpoints.Admin.Settings.Database;
using AspnetCoreStarter.Entities.Database;

namespace AspnetCoreStarter.Endpoints;
public static class ApiEndpointsExtension
{
  public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
  {
    new UsersEndpoints(routes, "/api/User", nameof(User));
    new PostTypessEndpoints(routes, "/api/PostType", nameof(PostType));
    new EntitiesEndpoints(routes, "/api/Entity", "Entitiy");
    new SchemaEndpoints(routes, "/api/Schema", "Schema");

    new DynamicTableConfigEndpoints(routes, "/api/Admin/Database/Tables", nameof(DynamicTableConfig));
  }
}

