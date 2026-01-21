using System.Diagnostics;
//using EateryCafe.Models;
using Microsoft.AspNetCore.Mvc;

namespace EateryCafe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
