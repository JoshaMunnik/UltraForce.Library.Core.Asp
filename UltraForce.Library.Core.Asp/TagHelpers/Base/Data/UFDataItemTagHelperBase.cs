// <copyright file="UFDataItemTagHelperBase.cs" company="Ultra Force Development">
// Ultra Force Library - Copyright (C) 2024 Ultra Force Development
// </copyright>
// <author>Josha Munnik</author>
// <email>josha@ultraforce.com</email>
// <license>
// The MIT License (MIT)
//
// Copyright (C) 2024 Ultra Force Development
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to 
// deal in the Software without restriction, including without limitation the 
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
// IN THE SOFTWARE.
// </license>

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Data;

/// <summary>
/// Base class for rendering a data definition. How it renders depends on the presence of
/// <see cref="For"/> and <see cref="Name"/> properties.
/// <para>
/// If <see cref="For"/> is set and the tag has no content, both the name and value
/// or obtained from the object referenced by <see cref="For"/>.
/// If the tag has content and <see cref="For"/> is set, only the name is obtained from
/// the object and the content is used for the value.
/// </para>
/// <para>
/// If <see cref="For"/> is not set, <see cref="Name"/> is used for the name and the value
/// from the content of the tag.
/// </para>
/// <para>
/// It renders the following:
/// <code>
/// &lt;dt class="{GetDataNameClasses()}"&gt;{For name|For name|Name}&lt;/dt&gt;
/// &lt;dd class="{GetDataValueClasses()}"&gt;{For value|content|content}&lt;/dd&gt;
/// </code>
/// </para> 
/// </summary>
public abstract class UFDataItemTagHelperBase(
  IUFModelExpressionRenderer modelExpressionRenderer
) : UFTagHelperWithModelExpressionRenderer(modelExpressionRenderer)
{
  #region public properties

  /// <summary>
  /// When no content is set, use the (display) name of the model property.
  /// </summary>
  public ModelExpression? For { get; set; }

  /// <summary>
  /// When set and <see cref="For" /> is <c>null</c>, the name content is set to this value and the
  /// content of this tag is used for the value part. If <see cref="For"/> is not <c>null</c>,
  /// this value is ignored.
  /// </summary>
  public string Name { get; set; } = "";

  #endregion

  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    await base.ProcessAsync(context, output);
    if (this.For == null)
    {
      this.RenderWithContentForDataAsync(output);
      return;
    }
    TagHelperContent? content = await output.GetChildContentAsync();
    if (content.IsEmptyOrWhiteSpace)
    {
      await this.RenderBothWithForAsync(output);
    }
    else
    {
      await this.RenderNameWithForAsync(output);
    }
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
  
  #endregion
  
  #region private methods
  
  /// <summary>
  /// Renders the content for the data part. The name part is set to the value of the
  /// <see cref="Name"/>.
  /// </summary>
  /// <param name="output"></param>
  private void RenderWithContentForDataAsync(
    TagHelperOutput output
  )
  {
    output.TagName = "dd";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFTagHelperTools.AddClasses(output, this.GetDataValueClasses());
    TagHelperOutput nameOutput = new(
      "dt",
      [],
      (
        useCachedResult,
        encoder
      ) =>
      {
        TagHelperContent value = new DefaultTagHelperContent();
        return Task.FromResult(value);
      }
    )
    {
      TagMode = TagMode.StartTagAndEndTag
    };
    nameOutput.Content.SetContent(this.Name);
    UFTagHelperTools.AddClasses(nameOutput, this.GetDataNameClasses());
    output.PreElement.AppendHtml(nameOutput);
  }

  /// <summary>
  /// Renders the content getting the name and data from the instance referenced by
  /// <see cref="For"/>.
  /// </summary>
  /// <param name="output"></param>
  private async Task RenderBothWithForAsync(
    TagHelperOutput output
  )
  {
    output.TagName = "dt";
    output.TagMode = TagMode.StartTagAndEndTag;
    await this.ModelExpressionRenderer.SetContentToNameAsync(output, this.For!, this.ViewContext);
    UFTagHelperTools.AddClasses(output, this.GetDataNameClasses());
    TagHelperOutput valueOutput = new(
      "dd",
      [],
      (
        useCachedResult,
        encoder
      ) =>
      {
        TagHelperContent value = new DefaultTagHelperContent();
        return Task.FromResult(value);
      }
    )
    {
      TagMode = TagMode.StartTagAndEndTag
    };
    await this.ModelExpressionRenderer.SetContentToValueAsync(
      valueOutput, this.For!, this.ViewContext
    );
    UFTagHelperTools.AddClasses(valueOutput, this.GetDataValueClasses());
    output.PostElement.AppendHtml(valueOutput);
  }


  /// <summary>
  /// Renders the content getting the name from the instance referenced by <see cref="For"/> and
  /// use the tags content for the data.
  /// </summary>
  /// <param name="output"></param>
  private async Task RenderNameWithForAsync(
    TagHelperOutput output
  )
  {
    output.TagName = "dd";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFTagHelperTools.AddClasses(output, this.GetDataValueClasses());
    TagHelperOutput nameOutput = new(
      "dt",
      [],
      (
        useCachedResult,
        encoder
      ) =>
      {
        TagHelperContent value = new DefaultTagHelperContent();
        return Task.FromResult(value);
      }
    )
    {
      TagMode = TagMode.StartTagAndEndTag
    };
    await this.ModelExpressionRenderer.SetContentToNameAsync(
      nameOutput, this.For!, this.ViewContext
    );
    UFTagHelperTools.AddClasses(nameOutput, this.GetDataNameClasses());
    output.PreElement.AppendHtml(nameOutput);
  }
  
  #endregion
}
