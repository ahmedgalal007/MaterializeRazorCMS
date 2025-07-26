
using AspnetCoreStarter.Common;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities.Database;
using AspnetCoreStarter.Repositories;
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
      .WithName("DynamicTableConfigFiltered")
      .WithOpenApi();


    _group.MapGet("/{id}", async (HttpRequest request, ApplicationSettingsDbContext context, [FromRoute] int id) =>
    {
      return await context.Set<DynamicTableConfig>().FindAsync(id);
      // return qService.Find(id);
    })
   .WithName("DynamicTableConfigGetById")
   .WithOpenApi();

    _group.MapPost("/",async (HttpRequest request, ApplicationSettingsDbContext context, [FromForm] DynamicTableConfig newTable) =>
    {
      await context.Set<DynamicTableConfig>().AddAsync(newTable);
      await context.SaveChangesAsync();
    })
   .WithName("DynamicTableConfigAdd")
   .WithOpenApi()
   .DisableAntiforgery();
  }
}
