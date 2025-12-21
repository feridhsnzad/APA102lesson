using Dinamic_Property.Models;
using Dinamic_Property.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dinamic_Property.Controllers
{
    public class HomeController : Controller
    {

        List<Student> students = new List<Student>
        { 
            new Student {Id = 1, Name="Elvin" , Age=19},
            new Student {Id = 2, Name="Ferid" , Age=20},
            new Student { Id = 3, Name ="Orxan", Age = 20 } 
        };

        List<Teacher> teachers = new List<Teacher>
        {

            new Teacher {Id=2 ,Name="Behruz",Surname="Aliyev"},
            new Teacher {Id=3 ,Name="ferid",Surname="hasanzada"},

        };

        public IActionResult Index()
        {
            //TempData["Name"] = "Elvin";
            HomeVM homeVM = new HomeVM()
            {
                Students = students,
                Teachers = teachers
            };
            return View(homeVM);


        }
        public IActionResult Test()
        {
            return Content("OK");
        }

        public IActionResult Details()
        {
            return View();
        }

          


        [Route("korporativ-satislar")]
        public IActionResult CorporativeSales()
        {
            return View();
        }
    }
} 
