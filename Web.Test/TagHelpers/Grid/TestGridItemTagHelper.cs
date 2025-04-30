using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid;

namespace Web.Test.TagHelpers.Grid;

public class TestGridItemTagHelper(
  IUFModelExpressionRenderer modelExpressionRenderer
) : UFGridItemTagHelperBase<TestGridTagHelper>(modelExpressionRenderer)
{
  #region protected methods

  protected override string GetItemClasses(
    TestGridTagHelper grid
  )
  {
    return "test-grid-item";
  }

  #endregion
}