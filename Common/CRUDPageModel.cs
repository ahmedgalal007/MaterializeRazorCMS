using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities;
using AspnetCoreStarter.Pages;

namespace AspnetCoreStarter.Common;

public class CRUDPageModel<T> : BasePageModel where T:class
{
  private readonly ApplicationDbContext _context;

  public CRUDPageModel(ApplicationDbContext context)
  {
    _context = context;
  }

  public virtual bool BeforeCreate(T entity)
  {
    return true;
  }
  public virtual bool BeforeUpdate(T entity)
  {
    return true;
  }

  public virtual bool BeforeDelete(T entity)
  {
    return true;
  }

  public virtual bool AfterCreate(T entity)
  {
    return true;
  }
  public virtual bool AfterUpdate(T entity)
  {
    return true;
  }

  public virtual bool AfterDelete(T entity)
  {
    return true;
  }
  /*
  public async Task<IActionResult> OnPostAsync()
  {
    if (NewUser?.UserName != null && NewUser?.Email != null)
    {
      // Add a new user to the database
      _context.User.Add(NewUser);
      await _context.SaveChangesAsync();
      return RedirectToPage();
    }

    return Page();
  }

  // The OnPostEditOrUpdateAsync method is called when the Edit User form is submitted
  public async Task<IActionResult> OnPostEditOrUpdateAsync(int id)
  {
    var userToUpdate = await _context.User.FindAsync(id);
    if (userToUpdate == null)
    {
      return NotFound();
    }

    // Update user properties based on form data
    string? isVerifiedString = Request.Form["user.IsVerified"];
    userToUpdate.IsVerified = (isVerifiedString == "on") ? true : false;

    // Update the user in the database and save changes
    await TryUpdateModelAsync(userToUpdate, "user", u => u.UserName, u => u.Email, u => u.IsVerified, u => u.ContactNumber, u => u.SelectedRole, u => u.SelectedPlan);
    await _context.SaveChangesAsync();
    return RedirectToPage();
  }

  // The OnPostDeleteAsync method is called when the Delete User form is submitted
  public async Task<IActionResult> OnPostDeleteAsync(int id)
  {
    var user = await _context.Set<T>().FindAsync(id);

    if (user != null)
    {
      // Remove the user from the database
      _context.User.Remove(user);
      await _context.SaveChangesAsync();
    }

    return RedirectToPage();
  }
  */
}
