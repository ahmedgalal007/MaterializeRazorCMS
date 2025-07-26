using AspnetCoreStarter.Entities;

namespace AspnetCoreStarter.Repositories;

public interface IRepositiory<Entity,TID> where Entity : class
{
  Task<int> CountAsync();
  Task<IEnumerable<Entity>> GetAll(int page=1, int size=10);
  Task<IQueryable<Entity>> QueryAll();
  Task<IQueryable<Entity>> Search();
  Task<Entity> GetById(TID id);
  Task<Entity> Add(Entity entity);
  Task<Entity> Update(Entity entity);
  Task<Entity> Delete(TID id);
  Task SaveChangesAsync();

}
