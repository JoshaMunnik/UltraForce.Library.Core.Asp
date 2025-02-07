using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base;
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Data;

/// <summary>
/// Base class for rendering a data definition. For the name part if <see cref="For"/> is set, the
/// content is set to the name of the referenced element. Else the children of this tag are
/// rendered. For the data part the content is set to the value of the referenced element. If
/// <see cref="For"/> is null, the data part is empty.
/// <para>
/// It renders the following:
/// <code>
/// &lt;dt class="{GetDataNameClasses()}"&gt;{content|For name}&lt;/dt&gt;
/// &lt;dd class="{GetDataValueClasses()}"&gt;{empty|For value}&lt;/dd&gt;
/// </code>
/// </para> 
/// </summary>
public abstract class UFDataItemTagHelper(
  IUFModelExpressionRenderer aModelExpressionRenderer
) : UFTagHelperWithModelExpressionRenderer(aModelExpressionRenderer)
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
  /// The default implementation returns an empty string.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetDataNameClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// The default implementation returns an empty string.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetDataValueClasses()
  {
    return string.Empty;
  }
}

#endregion