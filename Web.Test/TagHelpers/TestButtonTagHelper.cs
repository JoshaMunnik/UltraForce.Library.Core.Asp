using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Buttons;

namespace Web.Test.TagHelpers;

[HtmlTargetElement("test-button")]
public class TestButtonTagHelper(
  EndpointDataSource anEndpointDataSource,
  IHtmlGenerator aHtmlGenerator,
  IUFModelExpressionRenderer aModelExpressionRenderer
) : UFButtonTagHelperBase(anEndpointDataSource, aHtmlGenerator, aModelExpressionRenderer);