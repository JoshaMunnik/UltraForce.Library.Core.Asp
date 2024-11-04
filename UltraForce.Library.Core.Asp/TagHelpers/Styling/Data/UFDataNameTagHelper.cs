// <copyright file="UFDataNameTagHelper.cs" company="Ultra Force Development">
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

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Lib;
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Data;

/// <summary>
/// Base class for rendering a data definition. 
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[HtmlTargetElement("uf-data-name")]
public class UFDataNameTagHelper(
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

  #endregion
}