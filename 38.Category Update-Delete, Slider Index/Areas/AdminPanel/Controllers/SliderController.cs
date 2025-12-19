using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.Utilities.Enums;
using FrontToBack.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FrontToBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            List<Slider> slider = await _context.Sliders.ToListAsync();

            return View(slider);
        }
       
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Slider slider) 
        {
            if (!slider.Photo.ValidatorType(slider.Photo,"image/"))
            {
                ModelState.AddModelError("Photo", "Fyle type is incorrect");
                return View();
            }

            if (!slider.Photo.ValidatorSize(FileSize.MB,slider.Photo.Length))
            {
                ModelState.AddModelError("Photo", "File size must be less than 2 mb");
                return View();
            }

            slider.UrlImage = await slider.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");

            await _context.Sliders.AddAsync(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
