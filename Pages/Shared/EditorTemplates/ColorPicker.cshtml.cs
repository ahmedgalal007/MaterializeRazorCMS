using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreStarter.Pages.Shared.EditorTemplates;
public class ColorPickerModel
{
  public ColorPickerModel(string name, string id, string elSelectorId, string defaultColor = "#ffffff", string theme = "classic", List<string> swatches = null, PickrComponents components = null)
  {
    Name = name;
    Id = id;
    ElSelectorId = elSelectorId;
    DefaultColor = defaultColor;
    Theme = theme;
    Swatches = swatches ??= [
      "rgba(102, 108, 232, 1)",
      "rgba(40, 208, 148, 1)",
      "rgba(255, 73, 97, 1)",
      "rgba(255, 145, 73, 1)",
      "rgba(30, 159, 242, 1)"
    ];
    Components = components ??= new();

  }
  [Required]
  public string Id { get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  public string ElSelectorId { get; set; }
  public string DefaultColor { get; set; }
  public string Theme { get; set; } = "classic";
  public List<string> Swatches { get; set; }
  public PickrComponents Components { get; set; }
}

public class PickrComponents
{
  public PickrComponents(bool preview = true, bool opacity = true, bool hue = true, PickrInteraction interaction=null )
  {
    this.preview = preview;
    this.opacity = opacity;
    this.hue = hue;
    this.interaction = interaction ??= new();
  }
  public bool preview { get; set; } = true;
  public bool opacity { get; set; } = true;
  public bool hue { get; set; } = true;
  public PickrInteraction interaction { get; set; } = new();
}
public class PickrInteraction
{
  public bool hex { get; set; } = true;
  public bool rgba { get; set; } = true;
  public bool hsla { get; set; } = true;
  public bool hsva { get; set; } = true;
  public bool cmyk { get; set; } = true;
  public bool input { get; set; } = true;
  public bool clear { get; set; } = true;
  public bool save { get; set; } = true;
}

