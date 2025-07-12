using AspnetCoreStarter.Data;
using AspnetCoreStarter.Endpoints.Schemas;
using AspnetCoreStarter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AspnetCoreStarter.Services;

public class SchemaService(ApplicationSettingsDbContext context) : ISchemaService
{
  public readonly ApplicationSettingsDbContext _context = context;
  public async Task<DataTableSettings> GetSchema(String entityName)
  {
    var entity = await _context.TablesConfig
      .Include(e => e.Properties)
      .FirstAsync(e => e.EntityName == entityName);

    var createAction = new DataTableAction() { Name = "create"};
    var UpdateAction = new DataTableAction() { Name = "update" };
    var _actions = new List<DataTableAction> { createAction , UpdateAction };
    var columnDefs = new List<DataTableColumnDef> { };
    foreach (var prp in entity.Properties.OrderBy(e => e.Index))
    {
      var column=new DataTableColumn
      {
        Index = prp.Index,
        Data = prp.PropertyName,
        Options = new DataTableColumnOptions
        {
          Id = prp.Id,
          Name = prp.PropertyName,
          TypeName = prp.ControlName,
          ColSize = prp.ColSize,
        }
      };
      if (!prp.IsKey) createAction.Columns.Add(column);
      UpdateAction.Columns.Add(column);
      if (prp.ResponsivePriority > 0)
      {
        columnDefs.Add(new DataTableColumnDef
        {
          Targets = prp.Index,
          ResponsivePriority = prp.ResponsivePriority
        });
      }
    }
    return new DataTableSettings
    {
      Ajax = new AjaxOpt($"/api/Entity/{entityName}", "GET"),
      Actions = _actions,
      ColumnDefs = columnDefs
    };
  }
}
