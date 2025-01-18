using AspnetCoreStarter.Entities.Posts;
using AspnetCoreStarter.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspnetCoreStarter.Data;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace AspnetCoreStarter.Pages.Apps.Posts
{
  public class PostTypeModel : CRUDPageModel<PostType, Guid>
  {
    public List<Entities.Posts.Attribute> AttributeTypes { get; set; }
    public override async Task OnGetAsync(int page = 1, int take = 10)
    {
      CurrentPage = page;
      Total = await _dbSet.CountAsync();
      TableItems = await _dbSet
        .Include(e => e.Attributes)
        .Include(e => e.Childrens)
        .Include(e => e.Parent)
        .Skip(Start)
        .Take(take)
        .ToListAsync();
    }
    public PostTypeModel(ApplicationDbContext context) : base(context)
    {
    }


    public override Task<Boolean> UpdateEntity(PostType entity, String entityName = "")
    {
      
      throw new NotImplementedException();
    }
  }
}
