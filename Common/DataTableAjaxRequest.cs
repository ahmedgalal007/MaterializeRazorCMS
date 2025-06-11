using System.Collections.Specialized;
using System.Web;

namespace AspnetCoreStarter.Common;

public class DataTableAjaxResponse<T>
{
  public int Draw { get; set; } // The draw counter that DataTables uses to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables
  public int RecordsTotal { get; set; } // Total records, before filtering (i.e. the total number of records in the database)
  public int RecordsFiltered { get; set; } // Total records, after filtering (i.e. the total number of records after filtering has been applied)
  public IEnumerable<T> Data { get; set; } // The data to be displayed in the table
  public string Error { get; set; } // Optional: If there is an error, this message will be displayed by DataTables
}

// You'll also need a way to parse the DataTables request parameters.
// This is a simplified version, you might want to use a more robust library
// like "DataTables.AspNet.Core" for complex scenarios.
public class DataTableAjaxRequest
{
  public int Draw { get; set; }
  public int Start { get; set; } // Paging first instance to display (skip)
  public int Length { get; set; } // Paging number of records to display (take)
  public string SearchValue { get; set; } // Global search value
  public int OrderColumn { get; set; } // Column index to order by
  public string OrderDirection { get; set; } // Asc or Desc
  public List<Column> Columns { get; set; } = new List<Column>();
  public List<Order> Order { get; set; }  = new List<Order>();
  public Search Search { get; set; } = new Search();

  public static Boolean TryParse(string? value, out DataTableAjaxRequest? result)
  {
    result = GetParameters(value);
    return true;
  }
  public static Boolean TryParse(string? value, IFormatProvider? provider, out DataTableAjaxRequest? result)
  {
    result = GetParameters(value);
    return true;
  }

  public static DataTableAjaxRequest GetParameters(string? value)
  {
    NameValueCollection query = HttpUtility.ParseQueryString(value);
    return new DataTableAjaxRequest()
    {
      Draw = int.Parse(query["draw"]),
      Start = int.Parse(query["start"]),
      Length = int.Parse(query["length"]),

      Search = new Search()
      {
        Regex = bool.Parse(query["search[regex]"]),
        Value = query["search[value]"]
      },
      SearchValue = query["searchValue"],
      Order = new List<Order> {
        new Order{
          Column = int.Parse(query["order[0][column]"]),
          Dir = query["order[0][dir]"]
        }
      },
      Columns = GetColumns(query)
    };
   
  }

  public static List<Column> GetColumns(NameValueCollection query) {

    List<Column> columns = new List<Column>();
    List<int> idx = new List<int>();
    foreach (string key in query.Keys) {
      if (key.StartsWith("columns["))
      {
        string k = key.Replace("columns[", "");
        int col, pos = k.IndexOf(']');
        if(int.TryParse(k.Substring(0,pos),out col) && !idx.Any(e => e == col)) idx.Add(col);
      }
    }
    foreach (int i in idx)
    { bool SearchableRslt, OrderableRslt, SearchRegexRslt;
      string? data = query.Get($"columns[{i}][data]");
      string? name = query.Get($"columns[{i}][name]");
      string? searchable = query.Get($"columns[{i}][searchable]");
      string? orderable = query.Get($"columns[{i}][orderable]");
      string? searchRegex = query.Get($"columns[{i}][search][regex]");
      string? searchValue = query.Get($"columns[{i}][search][value]");
      columns.Add(new Column() {
        Data = data,
        Name = name,
        Searchable = bool.TryParse(searchable, out SearchableRslt) ? SearchableRslt : false,
        Orderable = bool.TryParse(orderable, out OrderableRslt) ? OrderableRslt : false,
        Search = new Search() {
          Regex = bool.TryParse(searchRegex, out SearchRegexRslt) ? SearchRegexRslt : false,
          Value = searchValue
        }
      });
    }
    return columns;
  }
  public class Column
  {
    public string Data { get; set; }
    public string Name { get; set; }
    public bool Searchable { get; set; }
    public bool Orderable { get; set; }
    public Search Search { get; set; } = new Search();
  }
}


public class Order
{
  public int Column { get; set; }
  public string Dir { get; set; }
}

public class Search
{
  public string Value { get; set; }
  public bool Regex { get; set; }
}
