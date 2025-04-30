using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid;

namespace Web.Test.TagHelpers.Grid;

[HtmlTargetElement("test-grid")]
public class TestGridTagHelper : UFGridTagHelperBase
{
  #region protected methods

  protected override string GetGridClasses()
  {
    return "test-grid";
  }

  protected override string GetFilterButtonClasses()
  {
    return "test-filter-button";
  }

  protected override string GetFilterCaptionHtml()
  {
    return "<span>Test Filter</span>";
  }
  
  protected override string GetFilterInputClasses()
  {
    return "test-filter-input";
  }

  protected override string GetFilterContainerClasses()
  {
    return "test-filter-container";  
  }
  
  protected override string GetFilterPlaceholder()
  {
    return "Test Filter Placeholder";
  }

  protected override string GetGridContainerClasses()
  {
    return "test-grid-container";
  }

  protected override string GetSortAscendingClasses()
  {
    return "test-grid-sort-ascending";
  }
  
  protected override string GetSortDescendingClasses()
  {
    return "test-grid-sort-descending";
  }

  #endregion
}