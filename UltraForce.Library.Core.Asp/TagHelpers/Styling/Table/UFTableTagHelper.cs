using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Lib;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Enums;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Table;

/// <summary>
/// Simple tag helper to render a table.
/// </summary>
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[HtmlTargetElement("uf-table", TagStructure = TagStructure.NormalOrSelfClosing)]
public class UFTableTagHelper(IUFTheme aTheme) : UFTagHelperWithTheme(aTheme), IUFTableProperties
{
  #region public constants

  /// <summary>
  /// The key that children can use to access this instance.
  /// </summary>
  public const string Table = "uf_table";

  #endregion

  #region IUFTableProperties

  /// <summary>
  /// When true add a `data-filter` attribute with the value "1" to the table.
  /// </summary>
  [HtmlAttributeName("filter")]
  public bool Filter { get; set; } = false;

  /// <summary>
  /// When true add a `data-paging` attribute with the value "1" to the table.
  /// </summary>
  [HtmlAttributeName("paging")]
  public bool Paging { get; set; } = false;

  /// <summary>
  /// Number of rows per page (only used if <see cref="Paging"/> is true). It will set
  /// `data-page-size` attribute on the table. 
  /// </summary>
  [HtmlAttributeName("page-size")]
  public int PageSize { get; set; } = 15;

  /// <summary>
  /// When true add `data-sorting` attribute with the value "1" to the table. 
  /// </summary>
  [HtmlAttributeName("sorting")]
  public bool Sorting { get; set; } = false;

  #endregion
  
  #region public properties

  /// <summary>
  /// Assign a value to preserve the sorting and paging state of the table between sessions. When
  /// set the state is stored in a local storage of the browser. 
  /// </summary>
  public string StorageId { get; set; } = "";
  
  #endregion

  #region public methods

  /// <inheritdoc />
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    base.Process(context, output);
    this.ProcessedFirstHeaderRow = null;
    context.Items[Table] = this;
    output.TagName = "table";
    string id = output.Attributes["id"]?.Value?.ToString() ?? UFHtmlTools.NewDomId();
    if (this.Filter)
    {
      this.AddFilter(output, id);
    }
    if (this.Paging)
    {
      output.Attributes.SetAttribute(UFDataAttribute.Paging, "1");
      output.Attributes.SetAttribute(UFDataAttribute.PageSize, this.PageSize.ToString());
    }
    if (this.Sorting)
    {
      this.AddSorting(output);
    }
    if ((this.Paging || this.Sorting) && !string.IsNullOrWhiteSpace(this.StorageId))
    {
      output.Attributes.SetAttribute(UFDataAttribute.StorageId, this.StorageId);
    }
    if (this.Filter || this.Paging || this.Sorting)
    {
      output.Attributes.SetAttribute("id", id);
    }
    UFTagHelperTools.AddClasses(output, this.Theme.GetTableClasses(this));
  }

  #endregion
  
  #region internal properties

  /// <summary>
  /// Will be set to true by the first header row.
  /// </summary>
  internal UFTableRowTagHelper? ProcessedFirstHeaderRow { get; set; }
  
  #endregion

  #region private methods

  private void AddFilter(TagHelperOutput anOutput, string aTableId)
  {
    anOutput.Attributes.SetAttribute(UFDataAttribute.Filter, "1");
    string inputId = UFHtmlTools.NewDomId();
    string input =
      $"<input id=\"{inputId}\" class=\"{this.Theme.GetFilterInputClasses()}\" placeholder=\"filter...\" type=\"text\" {UFDataAttribute.FilterTable}=\"#{aTableId}\" autocomplete=\"off\" />";
    string button =
      $"<button class=\"{this.Theme.GetFilterButtonClasses()}\" {UFDataAttribute.SetFieldSelector}=\"#{inputId}\">clear</button>";
    anOutput.PreElement.AppendHtml(
      $"<div><div class=\"{this.Theme.GetFilterContainerClasses()}\">{input}{button}</div>"
    );
    anOutput.PostElement.AppendHtml("</div>");
  }

  private void AddSorting(TagHelperOutput anOutput)
  {
    anOutput.Attributes.SetAttribute(UFDataAttribute.Sorting, "1");
    anOutput.Attributes.SetAttribute(
      UFDataAttribute.SortAscending, this.Theme.GetSortAscendingClasses()
    );
    anOutput.Attributes.SetAttribute(
      UFDataAttribute.SortDescending, this.Theme.GetSortDescendingClasses()
    );
  }

  #endregion
}