using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

namespace Web.Test.TagHelpers.Table;

[HtmlTargetElement("test-table-data-cell")]
public class TestTableDataCellTagHelper(
  IUFModelExpressionRenderer modelExpressionRenderer
) : UFTableDataCellTagHelperBase<TestTableTagHelper, TestTableDataRowTagHelper>(modelExpressionRenderer)
{
  protected override string GetTableCellClasses(
    TestTableTagHelper table,
    TestTableDataRowTagHelper tableRow
  )
  {
    return "test-table-data-cell";
  }
}