using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AspnetCoreStarter.Entities;
using Attribute = AspnetCoreStarter.Entities.Posts.Attribute;
using AspnetCoreStarter.Common;
using AspnetCoreStarter.Entities.Database;

namespace AspnetCoreStarter.Data
{
  public static class SeedTablesConfig
  {
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
      using (var configDb = new ApplicationSettingsDbContext(
          serviceProvider.GetRequiredService<DbContextOptions<ApplicationSettingsDbContext>>()))
      {


        // Check if there is already data in the database
        if (configDb.TablesConfig.Any() && configDb.AppSettings.Any())
        {
          return; // Database has been seeded
        }

        if (!configDb.TablesConfig.Any())
        {
          // Seed some sample data
          var tablesConfig = new DynamicTableConfig[]
          {
             new DynamicTableConfig
            {
                EntityName = "Customer",
                TableName = "Customers",
                Properties = new List<DynamicPropertyConfig>
                {
                    new DynamicPropertyConfig { PropertyName = "Id", PropertyType = "int", IsKey = true, IsRequired = true,ControlName="TextField", Index=0  },
                    new DynamicPropertyConfig { PropertyName = "Name", PropertyType = "string",IsKey = false, IsRequired = true ,ControlName="TextField" , Index=1},
                    new DynamicPropertyConfig { PropertyName = "Email", PropertyType = "string" ,IsKey = false, IsRequired = false,ControlName="TextField", Index=2 }
                }
            },
            new DynamicTableConfig
            {
              EntityName = "Order",
              TableName = "Orders",
              Properties = new List<DynamicPropertyConfig>
                {
                    new DynamicPropertyConfig { PropertyName = "Id", PropertyType = "int", IsKey = true, IsRequired = true,ControlName="TextField", Index=0   },
                    new DynamicPropertyConfig { PropertyName = "OrderDate", PropertyType = "datetime",IsKey = false, IsRequired = true ,ControlName="TextField", Index=1 },
                    new DynamicPropertyConfig { PropertyName = "TotalAmount", PropertyType = "decimal",IsKey = false, IsRequired = true,ControlName="TextField", Index=2  }
                }
            }
            // Add more user data as needed below...
          };

          configDb.TablesConfig.AddRange(tablesConfig);
          await configDb.SaveChangesAsync();
        }
      }
    }
  }
}
