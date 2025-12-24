 using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Controllers
{
    public class ShopController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
