using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

namespace Web.Test.TagHelpers.Table;

[HtmlTargetElement("test-table-header-cell")]
public class TestTableHeaderCellTagHelper(
  IUFModelExpressionRenderer modelExpressionRenderer
) : UFTableHeaderCellTagHelperBase<TestTableTagHelper, TestTableHeaderRowTagHelper>(modelExpressionRenderer)
{
  protected override string GetTableCellClasses(
    TestTableTagHelper table,
    TestTableHeaderRowTagHelper tableRow
  )
  {
    return "test-table-header-cell";
  }
}