using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

namespace Web.Test.TagHelpers.Table;

[HtmlTargetElement("test-table-header-row")]
public class TestTableHeaderRowTagHelper : UFTableHeaderRowTagHelperBase<TestTableTagHelper>
{
  protected override string GetTableRowClasses(
    TestTableTagHelper table
  )
  {
    return "test-table-header-row";
  }
}