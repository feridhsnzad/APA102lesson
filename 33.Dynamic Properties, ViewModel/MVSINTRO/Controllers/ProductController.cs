using Microsoft.AspNetCore.Mvc;

namespace MVSINTRO.Controllers
{
    public class ProductController: Controller
    {
        public IActionResult Index()
        {
            return View();
        } 
    }
}
