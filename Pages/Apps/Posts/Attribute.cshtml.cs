using AspnetCoreStarter.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;

namespace AspnetCoreStarter.Pages.Apps.Posts;

public class AttributeModel : BasePageModel
{
  private readonly ApplicationDbContext _context;

  // The UserCRUDModel constructor accepts a UserContext object as a parameter

  public AttributeModel(ApplicationDbContext context)
  {
    // Assign the UserContext object to the _context field
    _context = context;
    AvailableBaseTypestList = new SelectList(new List<string>()
    {
      typeof(bool).Name,
      typeof(decimal).Name,
      typeof(double).Name,
      typeof(DateTime).Name,
      typeof(DateOnly).Name,
      typeof(string).Name,
      typeof(TimeSpan).Name
    });
    NewAttribute = new Entities.Posts.Attribute();
  }

  [BindProperty]
  public Entities.Posts.Attribute NewAttribute { get; private set; }

  public List<Entities.Posts.Attribute> Attributes { get; set; }
  
  public SelectList AvailableBaseTypestList { get; set; }
  
  public void OnGet() {
    Attributes = _context.Attributes.AsNoTracking().ToList();
    AvailableBaseTypestList.Concat(new SelectList(Attributes.Select(e => e.Name).ToList()));
  }

  public async Task<ActionResult> OnPostAsync(Entities.Posts.Attribute attr)
  {
    if (NewAttribute?.Name != null && NewAttribute?.BaseType != null)
    {
      // Add a new Attribute to the database
      // _context.Attributes.Add(attr);
      // await _context.SaveChangesAsync();
      return RedirectToPage();
    }
    return Page();
  }

  public async Task<IActionResult> OnPostEditOrUpdateAsync(Guid id)
  {
    var attrToUpdate = await _context.Attributes.FindAsync(id);
    if (attrToUpdate == null)
    {
      return NotFound();
    }

    // Update user properties based on form data
    string? isVerifiedString = Request.Form["attribute.IsVerified"];
    // attrToUpdate.IsVerified = (isVerifiedString == "on") ? true : false;

    // Update the user in the database and save changes
    await TryUpdateModelAsync(attrToUpdate, "attribute", u => u.Name, u => u.Description, u => u.BaseType, u => u.ReturnType, u => u.Min, u => u.Max,  u => u.Format);
    await _context.SaveChangesAsync();
    return RedirectToPage();
  }

  public async Task<IActionResult> OnPostDeleteAsync(int id)
  {
    var attr = await _context.Attributes.FindAsync(id);

    if (attr != null)
    {
      // Remove the Attribute from the database
      // _context.Attributes.Remove(attr);
      // await _context.SaveChangesAsync();
    }

    return RedirectToPage();
  }
}