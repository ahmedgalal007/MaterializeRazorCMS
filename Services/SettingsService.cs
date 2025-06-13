namespace AspnetCoreStarter.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities;
using System.Text.Json;
using AspnetCoreStarter.Services.Interfaces;
// Services/SettingService.cs
using Microsoft.EntityFrameworkCore;
// For generic type conversion
public class SettingService : ISettingService
{
  private readonly ApplicationSettingsDbContext _dbContext;

  public SettingService(ApplicationSettingsDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<string?> GetSettingAsync(string key)
  {
    var setting = await _dbContext.AppSettings.FirstOrDefaultAsync(s => s.Key == key);
    return setting?.Value;
  }

  public async Task<T?> GetSettingAsync<T>(string key)
  {
    var stringValue = await GetSettingAsync(key);
    if (stringValue == null)
    {
      return default(T);
    }

    try
    {
      // Attempt to convert directly if T is string, int, bool etc.
      if (typeof(T) == typeof(string))
      {
        return (T)(object)stringValue;
      }
      if (typeof(T).IsPrimitive || typeof(T) == typeof(decimal) || typeof(T) == typeof(DateTime))
      {
        return (T)Convert.ChangeType(stringValue, typeof(T));
      }

      // For complex objects, assume JSON serialization
      return JsonSerializer.Deserialize<T>(stringValue);
    }
    catch
    {
      // Log the error or handle invalid conversion
      return default(T);
    }
  }


  public async Task SaveSettingAsync(string key, string value, string? description = null)
  {
    var existingSetting = await _dbContext.AppSettings.FirstOrDefaultAsync(s => s.Key == key);

    if (existingSetting != null)
    {
      existingSetting.Value = value;
      existingSetting.Description = description ?? existingSetting.Description;
      existingSetting.LastModified = DateTime.UtcNow;
    }
    else
    {
      _dbContext.AppSettings.Add(new AppSetting
      {
        Key = key,
        Value = value,
        Description = description ?? string.Empty,
        LastModified = DateTime.UtcNow
      });
    }
    await _dbContext.SaveChangesAsync();
  }

  public async Task<IEnumerable<AppSetting>> GetAllSettingsAsync()
  {
    return await _dbContext.AppSettings.ToListAsync();
  }

  public async Task DeleteSettingAsync(string key)
  {
    var setting = await _dbContext.AppSettings.FirstOrDefaultAsync(s => s.Key == key);
    if (setting != null)
    {
      _dbContext.AppSettings.Remove(setting);
      await _dbContext.SaveChangesAsync();
    }
  }
}
