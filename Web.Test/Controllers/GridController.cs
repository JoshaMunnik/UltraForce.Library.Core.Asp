using Microsoft.AspNetCore.Mvc;

namespace Web.Test.Controllers;

public class GridController : Controller
{
  public IActionResult Index()
  {
    return this.View();
  }
}