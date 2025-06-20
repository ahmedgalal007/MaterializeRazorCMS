// Services/ISettingService.cs
using static AspnetCoreStarter.Services.DynamicQueryService;

namespace AspnetCoreStarter.Services.Interfaces;
public interface IDynamicQueryService {
  Task<object> GetFilteredEntitiesAsync(
       string entityName,
       PropertyFilter[] filters, int start = 0, int length = 10, int draw = 0);
}

// Services/ISettingService.cs
