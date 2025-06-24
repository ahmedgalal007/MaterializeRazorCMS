using AspnetCoreStarter.Common;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities;
using AspnetCoreStarter.Services;
using AspnetCoreStarter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using static AspnetCoreStarter.Services.DynamicQueryService;

namespace AspnetCoreStarter.Endpoints.Users;

public class EntitiesEndpoints
{
  private readonly RouteGroupBuilder _group;
  public EntitiesEndpoints(IEndpointRouteBuilder routes, string baseRouteUrl, string routeTagName)
  {
    _group = routes.MapGroup(baseRouteUrl).WithTags(routeTagName);
    RegisterGroupEndpoints();
  }

  public void RegisterGroupEndpoints()
  {
    _group.MapGet("/{entityName}", async (HttpRequest request, IDynamicQueryService qService, [FromRoute] string entityName) =>
    {
      DataTableAjaxRequest.TryParse(request.QueryString.Value, out DataTableAjaxRequest dtReq);
      
      var _filters = new PropertyFilter[]
      {
        // new PropertyFilter("name","a", "contains")
      };
      var results = await qService.GetFilteredEntitiesAsync(entityName, _filters, dtReq.Start, dtReq.Length, dtReq.Draw );
      return results;
    })
      .WithName("EntitiesSearch")
      .WithOpenApi();


    _group.MapGet("/{entityName}/{id}", (HttpRequest request, IDynamicQueryService qService,[FromRoute] string entityName,[FromRoute] int id) =>
    {
      // var entityName = request.Query["entityName"];
      // return qService.Find(id);
    })
   .WithName("GetEntityByNameAndId")
   .WithOpenApi();
  }
}
