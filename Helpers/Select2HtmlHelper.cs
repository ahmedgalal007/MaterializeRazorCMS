using AspnetCoreStarter.Pages.Shared.EditorTemplates;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AspnetCoreStarter.Helpers;

public static class Select2HtmlHelper
{
  static Object GenerateSelect2ViewDataDictionary(string inputName, string inputId, List<KeyValuePair<string, string>> options, string cssClasses="", Dictionary<string, Object> attributes = default)
  {
    return new
    {
      Select2Model = new Select2Model(inputName, options, cssClasses, false, false, [], inputId, attributes)
    };
  }
  public static IHtmlContent Select2EditorFor<TModel, TResult>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TResult>> expression, String htmlFieldName, List<KeyValuePair<string, string>> options, string label = "", string htmlFieldId = "", string cssClasses="", Dictionary<string,Object> attributes = default, String templateName = "Select2")
  // where TModel : class
  {
    // helper.ViewData.TryAdd("InputName", htmlFieldName.Replace(".",""));

    // helper.ViewData.TryAdd(htmlFieldName.Replace(".", ""),  new Select2Model(htmlFieldName, options, false, false, [], htmlFieldId));
    // helper.EditorForModel(GenerateSelect2ViewDataDictionary(htmlFieldName, htmlFieldId, options));
    var Select2Editor = HtmlHelperEditorExtensions.EditorFor(helper, expression, templateName, GenerateSelect2ViewDataDictionary(htmlFieldName, htmlFieldId, options, cssClasses,attributes));

    TextWriter tw = new StringWriter();
    Select2Editor.WriteTo(tw, HtmlEncoder.Default);
    tw.Write(String.Format("<label for=\"{0}\">{1}</label>", htmlFieldId, label));
    return new HtmlString(tw.ToString());
  }

  public static IHtmlContent Select2Editor<TModel, TResult>(this IHtmlHelper<TModel> helper, string inputName, String htmlFieldName, List<KeyValuePair<string, string>> options, string label = "", String htmlFieldId = "", string cssClasses = "", Dictionary<string, Object> attributes = default, String templateName = "Select2")
  {
    var Select2Editor = HtmlHelperEditorExtensions.Editor(helper, inputName, templateName, GenerateSelect2ViewDataDictionary(htmlFieldName, htmlFieldId, options, cssClasses, attributes));

    TextWriter tw = new StringWriter();
    Select2Editor.WriteTo(tw, HtmlEncoder.Default);
    tw.Write(String.Format("<label for=\"{0}\">{1}</label>", htmlFieldId, label));
    return new HtmlString(tw.ToString());
  }
}
