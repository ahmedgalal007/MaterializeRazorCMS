// Services/ISettingService.cs
using static AspnetCoreStarter.Services.DynamicQueryService;

namespace AspnetCoreStarter.Services.Interfaces;
public interface ISettingsQueryService
{
  Task<object> GetFilteredEntitiesAsync<T>(
      PropertyFilter[] filters, int start = 0, int length = 10, int draw = 0) where T : class;
}

// Services/ISettingService.cs
