// <copyright file="UFDataNameTagHelperBase.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Data;

/// <summary>
/// Base class for rendering a data definition. If <see cref="For"/> is set, the content is set to
/// the name of the referenced element. Else the children of this tag are rendered.
/// <para>
/// It renders the following:
/// <code>
/// &lt;dt class="{GetDataNameClasses()}"&gt;{content|For name}&lt;/dt&gt;
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public abstract class UFDataNameTagHelperBase<TDataList>(
  IUFModelExpressionRenderer modelExpressionRenderer
) : UFTagHelperWithModelExpressionRenderer(modelExpressionRenderer)
  where TDataList : UFDataListTagHelperBase
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
    TDataList dataList = UFTagHelperTools.GetItem<TDataList>(
      context, UFDataListTagHelperBase.DataList
    );
    output.TagName = "dt";
    output.TagMode = TagMode.StartTagAndEndTag;
    if (this.For != null)
    {
      await this.ModelExpressionRenderer.SetContentToNameAsync(output, this.For, this.ViewContext);
    }
    UFTagHelperTools.AddClasses(output, this.GetDataNameClasses(dataList));
  }

  #endregion

  #region overridable protected methods

  /// <summary>
  /// The default returns an empty string.
  /// </summary>
  /// <param name="dataList">
  /// The data list this item is part of.
  /// </param>
  /// <returns></returns>
  protected virtual string GetDataNameClasses(TDataList dataList)
  {
    return string.Empty;
  }

  #endregion
}
