using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Forms;

namespace Web.Test.TagHelpers;

[HtmlTargetElement("test-input")]
public class TestInputTagHelper(
  IHtmlGenerator generator
) : UFInputTagHelperBase(generator);