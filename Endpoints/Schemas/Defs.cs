using System.Collections.Generic;
using AspnetCoreStarter.Entities.Posts;

namespace AspnetCoreStarter.Endpoints.Schemas;
public class DataTableAction
{
  public string Name { get; set; }
  public List<DataTableColumn> Columns { get; set; }
}

public class DataTableColumnOptions
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string TypeName { get; set; }
  public int ColSize { get; set; }

}
public class DataTableColumn
{
  public int Index { get; set; }
  public string Data { get; set; }
  public DataTableColumnOptions Options { get; set; }= new DataTableColumnOptions();
}

public class DataTableColumnDef
{
  public int Targets { get; set; }
  public int ResponsivePriority { get; set; }
}

public class DataTableSettings
{
  public AjaxOpt Ajax { get; set; }
  // public List<DataTableColumn> Columns { get; set; }
  public List<DataTableAction> Actions { get; set; } = new List<DataTableAction>();
  public List<DataTableColumnDef> ColumnDefs { get; set; } = new List<DataTableColumnDef>();
}

public record AjaxOpt (string Url, string Type);
