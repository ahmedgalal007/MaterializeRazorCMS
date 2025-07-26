
using AspnetCoreStarter.Common;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities.Database;
using AspnetCoreStarter.Repositories;
using AspnetCoreStarter.Services;
using AspnetCoreStarter.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
namespace AspnetCoreStarter.Endpoints.Admin.Settings.Database;

public class AntiForgeryEndpoints
{
  private readonly RouteGroupBuilder _group;

  public AntiForgeryEndpoints(IEndpointRouteBuilder routes, string baseRouteUrl, string routeTagName)
  {
    _group = routes.MapGroup(baseRouteUrl).WithTags(routeTagName);
    RegisterGroupEndpoints();

  }

  public void RegisterGroupEndpoints()
  {
    // Get token
    _group.MapGet("/token", (IAntiforgery forgeryService, HttpContext context) =>
    {
      var tokens = forgeryService.GetAndStoreTokens(context);
      var xsrfToken = tokens.RequestToken!;
      return TypedResults.Content(xsrfToken, "text/plain");
    });


  }
}
