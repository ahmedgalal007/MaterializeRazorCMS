using AspnetCoreStarter.Common;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities;
using AspnetCoreStarter.Services;
using AspnetCoreStarter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using static AspnetCoreStarter.Services.DynamicQueryService;

namespace AspnetCoreStarter.Endpoints.Schemas;

public class SchemaEndpoints
{
  private readonly RouteGroupBuilder _group;
  public SchemaEndpoints(IEndpointRouteBuilder routes, string baseRouteUrl, string routeTagName)
  {
    _group = routes.MapGroup(baseRouteUrl).WithTags(routeTagName);
    RegisterGroupEndpoints();
  }

  public void RegisterGroupEndpoints()
  {
    //  /api/PostType/GetAllPostTypes?page=1&take=10
    _group.MapGet("/{entityName}", async ( ISchemaService schemaService, [FromRoute]string entityName) =>
    {

      var results = await schemaService.GetSchema(entityName);
      return results;
    })
      .WithName("GetSchema")
      .WithOpenApi();
  }
}
