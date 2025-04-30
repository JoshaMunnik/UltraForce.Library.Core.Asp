using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid;

namespace Web.Test.TagHelpers.Grid;

public class TestGridBodyTagHelper : UFGridBodyTagHelperBase<TestGridTagHelper>
{
  protected override string GetBodyClasses(
    TestGridTagHelper grid
  )
  {
    return "test-grid-body";
  }
}