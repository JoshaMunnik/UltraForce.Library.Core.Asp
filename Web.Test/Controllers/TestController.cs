using Microsoft.AspNetCore.Mvc;

namespace Web.Test.Controllers;

[Route("{controller}/{action}")]
public class TestController : Controller
{
  public IActionResult NoParameters()
  {
    return this.View();
  }

  [HttpGet("{first:guid}/{second}")]
  public IActionResult WithParameters(
    Guid first,
    string second
  )
  {
    return this.View();
  }
}