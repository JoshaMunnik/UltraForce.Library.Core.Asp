using Microsoft.AspNetCore.Mvc;

namespace Web.Test.Controllers;

public class TableController : Controller
{
  public IActionResult Index()
  {
    return this.View();
  }
}