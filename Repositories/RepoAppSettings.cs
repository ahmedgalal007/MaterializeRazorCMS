using AspnetCoreStarter.Data;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreStarter.Repositories;

public class RepoAppSettings<Entity,TID>: IRepositiory<Entity,TID> where Entity : class
{
  private readonly ApplicationSettingsDbContext _db;

  public RepoAppSettings(ApplicationSettingsDbContext dbContext) 
  {
    _db = dbContext;
  }

  public async Task<Entity> Add(Entity entity)
  {
    await _db.AddAsync(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<Entity> Update(Entity entity)
  {
    _db.Update(entity);
    return entity;
  }

  public async Task<Entity> Delete(TID id)
  {
    throw new NotImplementedException();
  }

  public async Task SaveChangesAsync()
  {
    throw new NotImplementedException();
  }

  public async Task<Int32> CountAsync()
  {
    return await _db.Set<Entity>().AsNoTracking().CountAsync();
  }

  public async Task<IEnumerable<Entity>> GetAll(Int32 page = 1, Int32 size = 10)
  {
    return await _db.Set<Entity>().Skip((page-1)*size).Take(size).AsNoTracking().ToListAsync();
  }

  public async Task<IQueryable<Entity>> QueryAll()
  {
    return  _db.Set<Entity>().AsQueryable().AsNoTracking();
  }

  public async Task<IQueryable<Entity>> Search()
  {
    return _db.Set<Entity>().AsQueryable().AsNoTracking();
  }

  public async Task<Entity> GetById(TID id)
  {
    return await _db.Set<Entity>().FindAsync(id);
  }
}
