using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid;

namespace Web.Test.TagHelpers.Grid;

[HtmlTargetElement("test-grid-group")]
public class TestGridGroupTagHelper : UFGridGroupTagHelperBase<TestGridTagHelper> 
{
  #region protected methods

  protected override string GetGroupClasses(
    TestGridTagHelper grid
  )
  {
    return "test-grid-group";
  }

  #endregion
}