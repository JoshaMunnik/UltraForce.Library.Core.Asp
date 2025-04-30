using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid;
using UltraForce.Library.Core.Asp.Types.Enums;

namespace Web.Test.TagHelpers.Grid;

[HtmlTargetElement("test-grid-control")]
public class TestGridControlTagHelper(
  IUFModelExpressionRenderer modelExpressionRenderer
) : UFGridControlTagHelperBase<TestGridTagHelper>(modelExpressionRenderer)
{
  #region protected methods

  protected override string GetControlClasses(
    TestGridTagHelper grid,
    UFSortTypeEnum sortType
  )
  {
    string classes = "test-grid-control";
    if (sortType != UFSortTypeEnum.None)
    {
      classes += " test-grid-control--is-button";
    }
    return classes;
  }

  #endregion
  
}