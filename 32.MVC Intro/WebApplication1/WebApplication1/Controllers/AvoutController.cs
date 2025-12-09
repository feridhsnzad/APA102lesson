using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class AvoutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}