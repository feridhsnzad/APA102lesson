using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class Dashboard: Controller

    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
