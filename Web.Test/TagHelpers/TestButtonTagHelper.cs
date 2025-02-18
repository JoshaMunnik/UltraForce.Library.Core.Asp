using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Buttons;

namespace Web.Test.TagHelpers;

[HtmlTargetElement("test-button")]
public class TestButtonTagHelper(
  EndpointDataSource endpointDataSource,
  IHtmlGenerator htmlGenerator,
  IUFModelExpressionRenderer modelExpressionRenderer
) : UFButtonTagHelperBase(endpointDataSource, htmlGenerator, modelExpressionRenderer);