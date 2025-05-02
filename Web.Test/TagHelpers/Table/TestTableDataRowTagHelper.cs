using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

namespace Web.Test.TagHelpers.Table;

[HtmlTargetElement("test-table-data-row")]
public class TestTableDataRowTagHelper : UFTableDataRowTagHelperBase<TestTableTagHelper>
{
  protected override string GetTableRowClasses(
    TestTableTagHelper table
  )
  {
    return "test-table-data-row";
  }
}