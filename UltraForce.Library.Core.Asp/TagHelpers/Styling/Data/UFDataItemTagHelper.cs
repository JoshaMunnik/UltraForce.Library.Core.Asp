using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Lib;
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Data;

/// <summary>
/// This tag helper renders both the name (dt tag) and the value (dd tag) of a data item.
/// </summary>
/// <param name="aModelExpressionRenderer"></param>
/// <param name="aTheme"></param>
public class UFDataItemTagHelper(
  IUFModelExpressionRenderer aModelExpressionRenderer,
  IUFTheme aTheme
) : UFTagHelperWithModelExpressionRenderer(aModelExpressionRenderer, aTheme)
{
  #region public properties

  /// <summary>
  /// When no content is set, use the (display) name of the model property.
  /// </summary>
  public ModelExpression? For { get; set; }

  #endregion

  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
  {
    await base.ProcessAsync(context, output);
    output.TagName = "dt";
    output.TagMode = TagMode.StartTagAndEndTag;
    if (this.For != null)
    {
      await this.ModelExpressionRenderer.SetContentToNameAsync(output, this.For, this.ViewContext);
    }
    UFTagHelperTools.AddClasses(output, this.GetDataNameClasses());
    TagHelperOutput valueOutput = new TagHelperOutput(
      "dd",
      [],
      (useCachedResult, encoder) =>
      {
        TagHelperContent value = new DefaultTagHelperContent();
        return Task.FromResult(value);
      }
    )
    {
      TagMode = TagMode.StartTagAndEndTag
    };
    if (this.For != null)
    {
      await this.ModelExpressionRenderer.SetContentToValueAsync(
        valueOutput, this.For, this.ViewContext
      );
    }
    UFTagHelperTools.AddClasses(valueOutput, this.GetDataValueClasses());
    output.PostElement.AppendHtml(valueOutput);
  }

  #endregion

  #region overridable protected methods

  /// <summary>
  /// The default implementation calls <see cref="IUFTheme.GetDataNameClasses"/>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetDataNameClasses()
  {
    return this.Theme.GetDataNameClasses();
  }

  /// <summary>
  /// The default implementation calls <see cref="IUFTheme.GetDataValueClasses"/>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetDataValueClasses()
  {
    return this.Theme.GetDataValueClasses();
  }
}

#endregion