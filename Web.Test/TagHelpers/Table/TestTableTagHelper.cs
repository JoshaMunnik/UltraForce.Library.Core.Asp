using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

namespace Web.Test.TagHelpers.Table;

[HtmlTargetElement("test-table")]
public class TestTableTagHelper : UFTableTagHelperBase
{
  protected override string GetTableClasses(
    int cellCount
  )
  {
    return "test-table";
  }
  
  protected override string GetSortAscendingClasses()
  {
    return "test-grid-sort-ascending";
  }
  
  protected override string GetSortDescendingClasses()
  {
    return "test-grid-sort-descending";
  }

}