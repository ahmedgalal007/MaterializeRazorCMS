// Services/ISettingService.cs
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Endpoints.Schemas;
using static AspnetCoreStarter.Services.DynamicQueryService;

namespace AspnetCoreStarter.Services.Interfaces;
public interface ISchemaService
{
  Task<DataTableSettings> GetSchema(string entityName);
}

// Services/ISettingService.cs
