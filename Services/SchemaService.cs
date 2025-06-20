using AspnetCoreStarter.Data;
using AspnetCoreStarter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreStarter.Services;

public class SchemaService(ApplicationSettingsDbContext context) : ISchemaService
{
  public readonly ApplicationSettingsDbContext _context = context;
  public async Task<DynamicTableConfig?> GetSchema(String entityName)
  {
    return await _context.TablesConfig
      .Include(e => e.Properties)
      .FirstAsync(e => e.EntityName == entityName);
  }
}
