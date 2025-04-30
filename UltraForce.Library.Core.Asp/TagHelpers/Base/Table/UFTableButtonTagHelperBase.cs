// <copyright file="UFTableButtonTagHelperBase.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Routing;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Buttons;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

/// <summary>
/// This class is the base class for all table button tag helpers.
/// </summary>
/// <param name="endpointDataSource"></param>
/// <param name="htmlGenerator"></param>
/// <param name="modelExpressionRenderer"></param>
/// <typeparam name="TTable">
/// Subclass of <see cref="UFTableTagHelperBase"/>.
/// </typeparam>
/// <typeparam name="TRow">
/// Any class (normally a subclass of <see cref="UFTableDataRowTagHelperBase{TTable}"/>).
/// </typeparam>
/// <typeparam name="TCell">
/// Any class (normally a subclass of <see cref="UFTableHeaderCellTagHelperBase{TTable,TTableRow}"/>).
/// </typeparam>
public abstract class UFTableButtonTagHelperBase<TTable, TRow, TCell>(
  EndpointDataSource endpointDataSource,
  IHtmlGenerator htmlGenerator,
  IUFModelExpressionRenderer modelExpressionRenderer
) : UFBaseButtonTagHelperBase(endpointDataSource, htmlGenerator, modelExpressionRenderer)
  where TTable : UFTableTagHelperBase
  where TRow : class
  where TCell : class
{
  #region protected methods

  /// <inheritdoc />
  protected sealed override string GetBeforeCaptionHtml(
    TagHelperContext context,
    TagHelperOutput output,
    bool hasCaption,
    bool isStatic
  )
  {
    return this.GetBeforeCaptionHtml(
      UFTagHelperTools.GetItem<TTable>(context, UFGridTagHelperBaseBase.Grid),
      UFTagHelperTools.GetItem<TRow>(context, UFGridTagHelperBaseBase.Row),
      UFTagHelperTools.GetItem<TCell>(context, UFGridTagHelperBaseBase.Cell),
      hasCaption,
      isStatic
    );
  }

  /// <inheritdoc />
  protected sealed override string GetAfterCaptionHtml(
    TagHelperContext context,
    TagHelperOutput output,
    bool hasCaption,
    bool isStatic
  )
  {
    return this.GetAfterCaptionHtml(
      UFTagHelperTools.GetItem<TTable>(context, UFGridTagHelperBaseBase.Grid),
      UFTagHelperTools.GetItem<TRow>(context, UFGridTagHelperBaseBase.Row),
      UFTagHelperTools.GetItem<TCell>(context, UFGridTagHelperBaseBase.Cell),
      hasCaption,
      isStatic
    );
  }

  /// <inheritdoc />
  protected sealed override string GetButtonClasses(
    TagHelperContext context,
    TagHelperOutput output,
    bool hasCaption,
    bool isStatic
  )
  {
    return this.GetButtonClasses(
      UFTagHelperTools.GetItem<TTable>(context, UFGridTagHelperBaseBase.Grid),
      UFTagHelperTools.GetItem<TRow>(context, UFGridTagHelperBaseBase.Row),
      UFTagHelperTools.GetItem<TCell>(context, UFGridTagHelperBaseBase.Cell),
      hasCaption,
      isStatic
    );
  }

  /// <inheritdoc />
  protected sealed override string GetButtonCaptionClasses(
    TagHelperContext context,
    TagHelperOutput output,
    bool isStatic
  )
  {
    return this.GetButtonCaptionClasses(
      UFTagHelperTools.GetItem<TTable>(context, UFGridTagHelperBaseBase.Grid),
      UFTagHelperTools.GetItem<TRow>(context, UFGridTagHelperBaseBase.Row),
      UFTagHelperTools.GetItem<TCell>(context, UFGridTagHelperBaseBase.Cell),
      isStatic
    );
  }

  /// <summary>
  /// The default implementation returns an empty string.
  /// </summary>
  /// <param name="table">
  /// Table the button is rendered within.
  /// </param>
  /// <param name="row">
  /// Row the button is rendered within.
  /// </param>
  /// <param name="cell">
  /// Cell the button is rendered within.
  /// </param>
  /// <param name="hasCaption">
  /// True if the button has any content.
  /// </param>
  /// <param name="isStatic">
  /// True if the button is rendered as a div element.
  /// </param>
  /// <returns></returns>
  protected virtual string GetBeforeCaptionHtml(
    TTable table,
    TRow row,
    TCell cell,
    bool hasCaption,
    bool isStatic
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// The default implementation returns an empty string.
  /// </summary>
  /// <param name="table">
  /// Table the button is rendered within.
  /// </param>
  /// <param name="row">
  /// Row the button is rendered within.
  /// </param>
  /// <param name="cell">
  /// Cell the button is rendered within.
  /// </param>
  /// <param name="hasCaption">
  /// True if the button has any content.
  /// </param>
  /// <param name="isStatic">
  /// True if the button is rendered as a div element.
  /// </param>
  /// <returns></returns>
  protected virtual string GetAfterCaptionHtml(
    TTable table,
    TRow row,
    TCell cell,
    bool hasCaption,
    bool isStatic
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// The default implementation returns an empty string.
  /// </summary>
  /// <param name="table">
  /// Table the button is rendered within.
  /// </param>
  /// <param name="row">
  /// Row the button is rendered within.
  /// </param>
  /// <param name="cell">
  /// Cell the button is rendered within.
  /// </param>
  /// <param name="isStatic">
  /// True if the button is rendered as a div element.
  /// </param>
  /// <returns></returns>
  protected virtual string GetButtonCaptionClasses(
    TTable table,
    TRow row,
    TCell cell,
    bool isStatic
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// The default implementation returns an empty string.
  /// </summary>
  /// <param name="table">
  /// Table the button is rendered within.
  /// </param>
  /// <param name="row">
  /// Row the button is rendered within.
  /// </param>
  /// <param name="cell">
  /// Cell the button is rendered within.
  /// </param>
  /// <param name="hasCaption">
  /// True if the button has any content.
  /// </param>
  /// <param name="isStatic">
  /// True if the button is rendered as a div element.
  /// </param>
  /// <returns></returns>
  protected virtual string GetButtonClasses(
    TTable table,
    TRow row,
    TCell cell,
    bool hasCaption,
    bool isStatic
  )
  {
    return string.Empty;
  }

  #endregion
}