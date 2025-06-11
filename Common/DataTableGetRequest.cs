//using System.Diagnostics.CodeAnalysis;
//using System.Web;
//using AspnetCoreStarter.Common;

//namespace AspnetCoreStarter.Common;
//public class DataTableRequestSearch
//{
//  public string value { get; set; }
//  public bool regex { get; set; }
//}
//public class DataTableRequestColumn
//{
//  public string data { get; set; } = string.Empty;
//  public string name { get; set; } = string.Empty;
//  public bool searchable { get; set; } = false;
//  public bool orderable { get; set; } = false;
//  public DataTableRequestSearch search { get; set; } = new DataTableRequestSearch();

//  //bool TryParse(string, IFormatProvider, out T)
//  //{
//  //  T = new DataTableRequestColumn() {

//  //  };
//  //  return true;
//  //}
//}
//public class DataTableGetRequest : IParsable<DataTableGetRequest>
//{
//  public int draw { get; set; } = 0;
//  public int start { get; set; } = 0;
//  public int length { get; set; } = 5;
//  public DataTableRequestColumn[] columns { get; set; } = Array.Empty<DataTableRequestColumn>();
//  public DataTableRequestSearch search { get; set; } = new DataTableRequestSearch();

//  public static DataTableGetRequest Parse(String s, IFormatProvider? provider)
//  {

//    var query = HttpUtility.ParseQueryString(s);
//    return new DataTableGetRequest()
//    {
//      draw = int.Parse(query["draw"]),
//      start = int.Parse(query["start"]),
//      length = int.Parse(query["length"]),
//      search = new DataTableRequestSearch()
//      {
//        regex = bool.Parse(query["search[regex]"]),
//        value = query["search[value]"]
//      },
//      columns = new DataTableRequestColumn[] { }
//    };
//  }

//  public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DataTableGetRequest result)
//  {
//    var query = HttpUtility.ParseQueryString(s);
//    result= new DataTableGetRequest()
//    {
//      draw = int.Parse(query["draw"]),
//      start = int.Parse(query["start"]),
//      length = int.Parse(query["length"]),
//      search = new DataTableRequestSearch()
//      {
//        regex = bool.Parse(query["search[regex]"]),
//        value = query["search[value]"]
//      },
//      columns = new DataTableRequestColumn[] { }
//    };
//    return true;
//  }
//}
