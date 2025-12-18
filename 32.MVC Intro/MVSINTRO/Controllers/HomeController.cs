using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVSINTRO.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return Content("apa 102");
            //var student = new JsonResult(
            //    new
            //    {
            //        Id = 1,
            //        Name = "ferid",
            //        Surname = "hesenzade"
            //    }
            //    );
            return View();

        }
        public IActionResult Detail(int? id)
        {
            if (id is null || id < 1)
            {
                return RedirectToAction(nameof(Error)); 
            }
            //return id;

            return View();
        }
         
        

        public IActionResult Error()
        {
            return View();
        }
    }
}
