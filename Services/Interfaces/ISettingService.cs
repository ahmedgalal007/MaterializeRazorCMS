// Services/ISettingService.cs

using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetCoreStarter.Entities;

namespace AspnetCoreStarter.Services.Interfaces;
public interface ISettingService
{
  Task<string?> GetSettingAsync(string key);
  Task<T?> GetSettingAsync<T>(string key); // Generic for type conversion
  Task SaveSettingAsync(string key, string value, string? description = null);
  Task<IEnumerable<AppSetting>> GetAllSettingsAsync();
  Task DeleteSettingAsync(string key);
}

// Services/ISettingService.cs
