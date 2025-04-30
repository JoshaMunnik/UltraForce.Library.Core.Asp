// <copyright file="UFGridItemTagHelperBaseBase.cs" company="Ultra Force Development">
// Ultra Force Library - Copyright (C) 2025 Ultra Force Development
// </copyright>
// <author>Josha Munnik</author>
// <email>josha@ultraforce.com</email>
// <license>
// The MIT License (MIT)
//
// Copyright (C) 2025 Ultra Force Development
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
using UltraForce.Library.Core.Asp.Types.Constants;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;

/// <summary>
/// Base class for items within a grid or table. 
/// </summary>
/// <param name="modelExpressionRenderer"></param>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
public abstract class UFGridItemTagHelperBaseBase(
  IUFModelExpressionRenderer modelExpressionRenderer
)
  : UFTagHelperWithModelExpressionRenderer(modelExpressionRenderer)
{
  #region public properties

  /// <summary>
  /// An expression to be evaluated against the current model. 
  /// <para>
  /// Date values are formatted using mysql format (so there is no confusion on month and
  /// day positions):
  /// "yyyy-mm-dd hh:mm:ss"
  /// </para>
  /// <para>
  /// If the type of this value is a `bool`, an attribute `data-uf-sort-value` is set with either
  /// `0` or `1`.
  /// </para>
  /// <para>
  /// The value is used as content for the item.
  /// </para>
  /// </summary>
  [HtmlAttributeName("for")]
  public ModelExpression? For { get; set; } = null;

  /// <summary>
  /// When not null and <see cref="For"/> has not been set, create an attribute `data-uf-sort-value`
  /// and assign this value to it. 
  /// </summary>
  [HtmlAttributeName("value")]
  public string? Value { get; set; } = null;

  /// <summary>
  /// When true an attribute with the name "data-uf-no-filter" is added.
  /// </summary>
  [HtmlAttributeName("no-filter")]
  public bool NoFilter { get; set; } = false;

  #endregion

  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    await base.ProcessAsync(context, output);
    context.Items[UFGridTagHelperBaseBase.Cell] = this;
    UFGridTagHelperBaseBase grid =
      (context.Items[UFGridTagHelperBaseBase.Grid] as UFGridTagHelperBaseBase)!;
    await this.ProcessForAsync(output);
    if (grid.Filter)
    {
      this.SetFilter(output);
    }
  }

  #endregion

  #region private methods

  /// <summary>
  /// Processes the <see cref="For"/> property. It determines the sort type and sets the
  /// content (if it has not been altered) to either the name or value.
  /// </summary>
  /// <param name="output">Output to update</param>
  /// <returns>Sort type</returns>
  private async Task ProcessForAsync(
    TagHelperOutput output
  )
  {
    if (this.For == null)
    {
      if (this.Value != null)
      {
        output.Attributes.SetAttribute(UFDataAttribute.SortValue(this.Value));
      }
      return;
    }
    Type type = this.For!.Metadata.UnderlyingOrModelType;
    await this.ModelExpressionRenderer.SetContentToValueAsync(
      output, this.For, this.ViewContext
    );
    if (type == typeof(bool))
    {
      bool value = (bool)this.For.Model;
      output.Attributes.SetAttribute(UFDataAttribute.SortValue(value ? "1" : "0"));
    }
    else if (type == typeof(bool?))
    {
      bool? value = (bool?)this.For.Model;
      output.Attributes.SetAttribute(
        UFDataAttribute.SortValue((value != null) && value.Value ? "1" : "0")
      );
    }
    if (!output.Attributes.ContainsName("title") && (this.For.Model != null))
    {
      output.Attributes.SetAttribute(
        "title", this.ModelExpressionRenderer.GetValueAsText(this.For, this.ViewContext)
      );
    }
  }

  /// <summary>
  /// Adds a <see cref="UFDataAttribute.Filter"/> attribute if the cell is a data cell and
  /// <see cref="NoFilter"/> is true.
  /// </summary>
  /// <param name="output"></param>
  private void SetFilter(
    TagHelperOutput output
  )
  {
    if (this.NoFilter)
    {
      output.Attributes.SetAttribute(UFDataAttribute.NoFilter("1"));
    }
  }

  #endregion
}