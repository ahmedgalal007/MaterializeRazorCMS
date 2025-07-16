using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities.Database;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Attribute = AspnetCoreStarter.Entities.Posts.Attribute;

namespace AspnetCoreStarter.Pages.Settings.Database;

public class TablesModel : BasePageModel
{
  private readonly ApplicationSettingsDbContext _context;

  // The UserCRUDModel constructor accepts a UserContext object as a parameter

  public TablesModel(ApplicationSettingsDbContext context)
  {
    // Assign the UserContext object to the _context field
    _context = context;

    AvailableBaseTypestList =
    [
      new() { Text = typeof(bool).Name , Value= typeof(bool).FullName},
      new() { Text = typeof(decimal).Name , Value= typeof(decimal).FullName},
      new() { Text = typeof(double).Name , Value= typeof(double).FullName},
      new() { Text = typeof(DateTime).Name , Value= typeof(DateTime).FullName},
      new() { Text = typeof(DateOnly).Name , Value= typeof(DateOnly).FullName},
      new() { Text = typeof(string).Name , Value= typeof(string).FullName},
      new() { Text = typeof(TimeSpan).Name , Value= typeof(TimeSpan).FullName}
    ];

    AvailableReturnTypestList = new SelectList(new List<string>()
    {
      typeof(bool).Name,
      typeof(decimal).Name,
      typeof(double).Name,
      typeof(DateTime).Name,
      typeof(DateOnly).Name,
      typeof(string).Name,
      typeof(TimeSpan).Name
    });

    NewEntity = new DynamicTableConfig();
    EntityList = [];
  }

  [BindProperty]
  public DynamicTableConfig NewEntity { get; set; }
  public string SeletedReturnType { get; set; }
  public List<DynamicTableConfig> EntityList { get; set; }
  
  public IEnumerable<SelectListItem> AvailableBaseTypestList { get; set; }
  public SelectList AvailableReturnTypestList { get; set; }
  
  public void OnGet() {
    EntityList = _context.TablesConfig.AsNoTracking().ToList();
    AvailableBaseTypestList.Concat(new SelectList(EntityList.Select(e => e.TableName).ToList()));
    // AvailableReturnTypestList.Concat(new SelectList(Attributes.Select(e => new { Text = e.Name, Value = e.ReturnType }).ToList()));
  }

  public async Task<IActionResult> OnPostAsync()
  {
    if (NewEntity?.TableName != null && NewEntity?.EntityName != null)
    {
      // Add a new Attribute to the database
      _context.TablesConfig.Add(NewEntity);
      await _context.SaveChangesAsync();
      return RedirectToPage();
    }
    return Page();
  }

  public async Task<IActionResult> OnPostEditOrUpdateAsync(Guid id)
  {
    var attrToUpdate = await _context.TablesConfig.FindAsync(id);
    if (attrToUpdate == null)
    {
      return NotFound();
    }

    // Update user properties based on form data
    string? isVerifiedString = Request.Form["attribute.IsVerified"];
    // attrToUpdate.IsVerified = (isVerifiedString == "on") ? true : false;

    // Update the user in the database and save changes
    await TryUpdateModelAsync(attrToUpdate, "attribute", u => u.TableName, u => u.EntityName);
    await _context.SaveChangesAsync();
    return RedirectToPage();
  }

  public async Task<IActionResult> OnPostDeleteAsync(int id)
  {
    var attr = await _context.TablesConfig.FindAsync(id);

    if (attr != null)
    {
      // Remove the Attribute from the database
      // _context.Attributes.Remove(attr);
      // await _context.SaveChangesAsync();
    }

    return RedirectToPage();
  }
}
