namespace AspnetCoreStarter.Services;

public struct PropertyFilter
{
  public PropertyFilter(string propertyName, object propertyValue, string comparisonType = "Equals")
  {
    PropertyName = propertyName;
    PropertyValue = propertyValue;
    ComparisonType = comparisonType;
  }

  public string PropertyName { get; set; }
  public object PropertyValue { get; set; }
  public string ComparisonType { get; set; } = "Equals";
}
