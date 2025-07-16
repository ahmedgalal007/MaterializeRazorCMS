
using AspnetCoreStarter.Common;
using AspnetCoreStarter.Entities.Database;
using AspnetCoreStarter.Services;
using AspnetCoreStarter.Services.Interfaces;
namespace AspnetCoreStarter.Endpoints.Admin.Settings.Database;

public class DynamicTableConfigEndpoints
{
  private readonly RouteGroupBuilder _group;
  public DynamicTableConfigEndpoints(IEndpointRouteBuilder routes, string baseRouteUrl, string routeTagName)
  {
    _group = routes.MapGroup(baseRouteUrl).WithTags(routeTagName);
    RegisterGroupEndpoints();
  }

  public void RegisterGroupEndpoints()
  {
    _group.MapGet("/", async (HttpRequest request, ISettingsQueryService qService) =>
    {
      DataTableAjaxRequest.TryParse(request.QueryString.Value, out DataTableAjaxRequest dtReq);

      var _filters = new PropertyFilter[]
      {
        // new PropertyFilter("name","a", "contains")
      };
      var results = await qService.GetFilteredEntitiesAsync<DynamicTableConfig>( _filters, dtReq.Start, dtReq.Length, dtReq.Draw);
      return results;
    })
      .WithName("DynamicTableConfig")
      .WithOpenApi();


    _group.MapGet("/{id}", (HttpRequest request, ISettingsQueryService qService, [FromRoute] int id) =>
    {
      // var entityName = request.Query["entityName"];
      // return qService.Find(id);
    })
   .WithName("GetDynamicTableConfigById")
   .WithOpenApi();
  }
}
