namespace AspnetCoreStarter.Entities.Posts;

public class PostTypeAttributeValidation : BaseEntity
{
  public Guid PostTypeAttributeId { get; set; } = new();
  public PostTypeAttribute PostTypeAttribute { get; set; } = new();
  public string Name { get; set; } = string.Empty;// min | max | length | notnull | regexp
  public string Value { get; set; } = string.Empty;
}

