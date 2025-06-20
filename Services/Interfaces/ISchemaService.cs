// Services/ISettingService.cs
using AspnetCoreStarter.Data;
using static AspnetCoreStarter.Services.DynamicQueryService;

namespace AspnetCoreStarter.Services.Interfaces;
public interface ISchemaService
{
  Task<DynamicTableConfig?> GetSchema(string entityName);
}

// Services/ISettingService.cs
