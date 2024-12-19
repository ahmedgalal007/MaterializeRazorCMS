using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities;
using AspnetCoreStarter.Pages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspnetCoreStarter.Common;

public abstract class CRUDPageModel<T,TID> : BasePageModel where T : class, new() where TID : struct
{

  private readonly ApplicationDbContext _context;

  //! Protected Variables
  protected readonly DbSet<T> _dbSet;
  public readonly Language? DefaultLanguage;
  public List<T> TableItems { get; set; }
  public int CurrentPage { get; set; } = 1;
  public int Take { get; set; } = 10;
  public int Start => Take * (CurrentPage - 1);
  public int End => Start + Take;
  public int Total { get; set; } = 0;

  [BindProperty]
  public T NewEntry { get; set; }

  public CRUDPageModel(ApplicationDbContext context)
  {
    _context = context;
    _dbSet = context.Set<T>();
    NewEntry = new T();
    Language DC = ISOLanguages.languages.FirstOrDefault(e => e.IsDefault);
    DefaultLanguage = _context.Languages.Where(x => x.IsoCode == DC.IsoCode).FirstOrDefault();
  }

  public string EntityName => typeof(T).Name;

  #region Before After CRUD
  public virtual async Task<bool> BeforeCreate(T entity)
  {
    return await Task.FromResult(true);
  }
  public virtual async Task<bool> BeforeUpdate(T entity)
  {
    return await Task.FromResult(true);
  }

  public virtual async Task<bool> BeforeDelete(T entity)
  {
    return await Task.FromResult(true);
  }

  public virtual async Task<bool> AfterCreate(T entity)
  {
    return await Task.FromResult(true);
  }
  public virtual async Task<bool> AfterUpdate(T entity)
  {
    return await Task.FromResult(true);
  }

  public virtual async Task<bool> AfterDelete(T entity)
  {
    return await Task.FromResult(true);
  }
  #endregion
  // Expression<Func<T, object?>>[] entityParams = [];
  public abstract Task<bool> UpdateEntity(T entity, string entityName = "");

  public virtual async Task OnGetAsync(int page = 1, int take = 10)
  {
    CurrentPage = page;
    Total = await _dbSet.CountAsync();
    TableItems = await _dbSet.Skip(Start).Take(take).ToListAsync();
  }


  public async Task<IActionResult> OnPostAsync()
  {

    if (!await BeforeCreate(NewEntry)) return Page();

    // Add a new Entity to the database
    _dbSet.Add(NewEntry);
    await _context.SaveChangesAsync();
    await AfterCreate(NewEntry);
    return RedirectToPage();

  }

  // The OnPostEditOrUpdateAsync method is called when the Edit User form is submitted
  public async Task<IActionResult> OnPostEditOrUpdateAsync(TID id)
  {
    var EntityToUpdate = await _dbSet.FindAsync(id);
    if (EntityToUpdate == null)
    {
      return NotFound();
    }

    // Update the entity in the database and save changes
    if (await BeforeUpdate(EntityToUpdate))
    {
      await UpdateEntity(EntityToUpdate);
      await _context.SaveChangesAsync();
      await AfterUpdate(EntityToUpdate);
    }
    return RedirectToPage();
  }

  // The OnPostDeleteAsync method is called when the Delete User form is submitted
  public async Task<IActionResult> OnPostDeleteAsync(TID id)
  {

    T? entityToDelete = await _dbSet.FindAsync(id);
    if (entityToDelete != null)
    {
      if (await BeforeDelete(entityToDelete))
      {
        // Remove the user from the database
        _dbSet.Remove(entityToDelete);
        await _context.SaveChangesAsync();
        await AfterDelete(entityToDelete);
      }
    }

    return RedirectToPage();
  }

}
