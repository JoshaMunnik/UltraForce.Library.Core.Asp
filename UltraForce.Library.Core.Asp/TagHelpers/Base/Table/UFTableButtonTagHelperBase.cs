using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Buttons;
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
/// Any class (normally a subclass of <see cref="UFTableRowTagHelperBase{TTable}"/>).
/// </typeparam>
/// <typeparam name="TCell">
/// Any class (normally a subclass of <see cref="UFTableCellTagHelperBase{TTable, TRow}"/>).
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
      UFTagHelperTools.GetItem<TTable>(context, UFTableTagHelperBase.Table),
      UFTagHelperTools.GetItem<TRow>(context, UFTableTagHelperBase.Row),
      UFTagHelperTools.GetItem<TCell>(context, UFTableTagHelperBase.Cell),
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
      UFTagHelperTools.GetItem<TTable>(context, UFTableTagHelperBase.Table),
      UFTagHelperTools.GetItem<TRow>(context, UFTableTagHelperBase.Row),
      UFTagHelperTools.GetItem<TCell>(context, UFTableTagHelperBase.Cell),
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
      UFTagHelperTools.GetItem<TTable>(context, UFTableTagHelperBase.Table),
      UFTagHelperTools.GetItem<TRow>(context, UFTableTagHelperBase.Row),
      UFTagHelperTools.GetItem<TCell>(context, UFTableTagHelperBase.Cell),
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
      UFTagHelperTools.GetItem<TTable>(context, UFTableTagHelperBase.Table),
      UFTagHelperTools.GetItem<TRow>(context, UFTableTagHelperBase.Row),
      UFTagHelperTools.GetItem<TCell>(context, UFTableTagHelperBase.Cell),
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