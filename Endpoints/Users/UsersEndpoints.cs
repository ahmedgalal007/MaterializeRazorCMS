using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities;

namespace AspnetCoreStarter.Endpoints.Users;

public class UsersEndpoints
{
  private readonly RouteGroupBuilder _group;
  public UsersEndpoints(IEndpointRouteBuilder routes, string baseRouteUrl, string routeTagName)
  {
    _group = routes.MapGroup(baseRouteUrl).WithTags(routeTagName);
    RegisterGroupEndpoints();
  }
  public void RegisterGroupEndpoints()
  {
    _group.MapGet("/", (ApplicationDbContext dbContext) =>
    {
      Random random = new Random();
      string[] billings = { "Manual - Credit Card", "Manual - Cash", "Manual - Paypal", "Auto Debit" };
      //return new[] { new User() };
      return new
      {
        data = dbContext.User.Select(e => new
        {
          id = e.Id,
          full_name = e.UserName,
          role = Char.ToUpperInvariant(e.SelectedRole[0]) + e.SelectedRole.Substring(1),
          username = e.UserName,
          email = e.Email,
          current_plan = e.SelectedPlan,
          billing = billings.GetValue(random.Next(0, 3)),
          status = random.Next(1, 3),
          avatar = random.Next(1, 10) + ".png"
        }).ToList()
      };
    })
      .WithName("GetAllUsers")
      .WithOpenApi();

    _group.MapGet("/{id}", (ApplicationDbContext dbContext, int id) =>
    {
      //return new User { ID = id };
      return dbContext.User.Find(id);
    })
    .WithName("GetUserById")
    .WithOpenApi();

    _group.MapPut("/{id}", (int id, User input) =>
    {
      return TypedResults.NoContent();
    })
    .WithName("UpdateUser")
    .WithOpenApi();

    _group.MapPost("/", (ApplicationDbContext dbContext, User model) =>
    {
      var aaa = model;
      //return TypedResults.Created($"/api/Users/{model.ID}", model);
    })
    .WithName("CreateUser")
    .WithOpenApi();

    _group.MapDelete("/{id}", (int id) =>
    {
      //return TypedResults.Ok(new User { ID = id });
    })
    .WithName("DeleteUser")
    .WithOpenApi();
  }
}
