using AspnetCoreStarter.Common;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using static AspnetCoreStarter.Endpoints.DynamicQueryService;

namespace AspnetCoreStarter.Endpoints.Users;

public class PostTypessEndpoints
{
  private readonly RouteGroupBuilder _group;
  public PostTypessEndpoints(IEndpointRouteBuilder routes, string baseRouteUrl, string routeTagName)
  {
    _group = routes.MapGroup(baseRouteUrl).WithTags(routeTagName);
    RegisterGroupEndpoints();
  }

  public object getResults(ApplicationDbContext dbContext, int page = 1, int take = 10)
  {
    return new
    {
      data = dbContext.PostTypes.Include(e => e.Parent).Skip((page - 1) * take).Take(take).Select(e => new
      {
        id = e.Id,
        name = e.Name,
        parent = e.Parent == null ? null : new
        {
          id = e.ParentId,
          name = e.Parent.Name
        },
        slug = e.Slug
      }).ToList()
    };
  }

  public object getFakeResults(int draw=0, int start=0, int length=10)
  {
    return new
    {
      draw = draw,
      recordsTotal = 10,
      recordsFiltered = 10,
      data = new[] {
        new { id = "aa4b1fd5-7a63-4e45-ab34-19356d73ac07", name = "PostType01", parent = new { id = "1", name = "a" }, email = "customermail01@gmail.com" },
        new { id = "5b679fe3-b98b-496c-908c-adade182760f", name = "PostType02", parent = new { id = "2", name = "b" }, email = "customermail02@gmail.com" },
        new { id = "76c3d4a0-c5dc-45e1-81c8-d66866e59229", name = "PostType03", parent = new { id = "3", name = "c" }, email = "customermail03@gmail.com" },
        new { id = "d94a24c9-d2ce-4460-9a00-f24a2170b982", name = "PostType04", parent = new { id = "4", name = "d" }, email = "customermail04@gmail.com" },
        new { id = "0a4fc4af-6715-43ed-a925-8129490b3e4f", name = "PostType05", parent = new { id = "5", name = "e" }, email = "customermail05@gmail.com" },
        new { id = "a2bcbf59-18f2-4b40-bc09-2414eda31085", name = "PostType06", parent = new { id = "6", name = "f" }, email = "customermail06@gmail.com" },
        new { id = "1630ddfd-eb41-4e5d-93ac-e1806c04a4cd", name = "PostType07", parent = new { id = "7", name = "g" }, email = "customermail07@gmail.com" },
        new { id = "91581de3-999f-4272-91e1-62bcd6b7f7e2", name = "PostType08", parent = new { id = "8", name = "h" }, email = "customermail08@gmail.com" },
        new { id = "7c3d4d90-edff-42ac-bb34-d11cebae3d8c", name = "PostType09", parent = new { id = "9", name = "i" }, email = "customermail09@gmail.com" },
        new { id = "58641857-921a-46fc-a0cc-56cd93d73775", name = "PostType10", parent = new { id = "10", name = "j" }, email = "customermail10@gmail.com" },
      }.Skip(start).Take(length)
      //data = new[] {
      //  new[] {"", "aa4b1fd5-7a63-4e45-ab34-19356d73ac07", "PostType01", "customermail01@gmail.com", "", ""},
      //  new[] {"", "5b679fe3-b98b-496c-908c-adade182760f", "PostType02", "customermail02@gmail.com", "", ""},
      //  new[] {"", "76c3d4a0-c5dc-45e1-81c8-d66866e59229", "PostType03", "customermail03@gmail.com", "", ""},
      //  new[] {"", "d94a24c9-d2ce-4460-9a00-f24a2170b982", "PostType04", "customermail04@gmail.com", "", ""},
      //  new[] {"", "0a4fc4af-6715-43ed-a925-8129490b3e4f", "PostType05", "customermail05@gmail.com", "", ""},
      //  new[] {"", "a2bcbf59-18f2-4b40-bc09-2414eda31085", "PostType06", "customermail06@gmail.com", "", ""},
      //  new[] {"", "1630ddfd-eb41-4e5d-93ac-e1806c04a4cd", "PostType07", "customermail07@gmail.com", "", ""},
      //  new[] {"", "91581de3-999f-4272-91e1-62bcd6b7f7e2", "PostType08", "customermail08@gmail.com", "", ""},
      //  new[] {"", "7c3d4d90-edff-42ac-bb34-d11cebae3d8c", "PostType09", "customermail09@gmail.com", "", ""},
      //  new[] {"", "58641857-921a-46fc-a0cc-56cd93d73775", "PostType10", "customermail10@gmail.com", "", ""},
      //}
    };
  }
  public void RegisterGroupEndpoints()
  {
    //  DataTableRequestColumn[] columns, int start = 1, int length = 10, int draw = 0
    //  /api/PostType/GetAllPostTypes?page=1&take=10
    _group.MapGet("/", async (HttpRequest request, DynamicQueryService qService) =>
    {
      // var jsonQyery = await request.ReadFromJsonAsync<DataTableAjaxRequest>();
      // DataTableAjaxRequest dtReq;
      DataTableAjaxRequest.TryParse(request.QueryString.Value, out DataTableAjaxRequest dtReq);
      // return getResults(dbContext, page, take);
      // return getFakeResults(dtReq.Draw, dtReq.Start, dtReq.Length);
      var _filters = new PropertyFilter[]
      {
        new PropertyFilter("name","a", "contains")
      };
      var results = await qService.GetFilteredEntitiesAsync(request.Query["entityName"], _filters, dtReq.Start, dtReq.Length, dtReq.Draw );
      return results;
    })
      .WithName("GetAllPostTypes")
      .WithOpenApi();


    _group.MapGet("/{id}", (ApplicationDbContext dbContext, int id) =>
    {
      //return new User { ID = id };
      return dbContext.PostTypes.Find(id);
    })
   .WithName("GetPostTypeById")
   .WithOpenApi();
  }
}
