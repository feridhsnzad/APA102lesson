using FrontToBack.Areas.AdminPanel.ViewModels;
using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.Utilities.Enums;
using FrontToBack.Utilities.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FrontToBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,Moderator,Member")]

    public class SliderController : Controller
    {

        private readonly AppDbContext _context;

        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetSLiderVM> sliders = await _context.Sliders
                .Select(s => new GetSLiderVM
                {
                    Id = s.Id,
                    Title = s.Title,
                    ImageUrl = s.ImageUrl,
                    Order = s.Order
                })
                .ToListAsync();
            return View(sliders);
        }
        [Authorize(Roles = "Admin,Moderator")]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Create(SliderCreateVM sliderCreateVM)
        {
            if (!ModelState.IsValid) return View();

            if (!sliderCreateVM.Photo.ValidatorType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is incorrect!");
                return View();
            }
            if (!sliderCreateVM.Photo.ValidatorSize(FileSize.MB, sliderCreateVM.Photo.Length))
            {
                ModelState.AddModelError("Photo", "File size must be less than 2MB!");
                return View();
            }

            Slider slider = new()
            {
                Title = sliderCreateVM.Title,
                SubTitle = sliderCreateVM.SubTitle,
                Description = sliderCreateVM.Description,
                Order = sliderCreateVM.Order,
                ImageUrl = await sliderCreateVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images")
            };


            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);

            if (slider == null) return NotFound();

            slider.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id < 1) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            DetailsSliderVM detailsSliderVM = new()
            {
                Id = slider.Id,
                Title = slider.Title,
                SubTitle = slider.SubTitle,
                Order = slider.Order,
                ImageUrl = slider.ImageUrl
            };
            return View(detailsSliderVM);
        }
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();

            SliderUpdateVM sliderUpdateVM = new()
            {
                Title = slider.Title,
                SubTitle = slider.SubTitle,
                Description = slider.Description,
                Order = slider.Order,
                ImageUrl = slider.ImageUrl
            };
            return View(sliderUpdateVM);

        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Update(int? id, SliderUpdateVM sliderUpdateVM)
        {
            if (id is null || id < 1) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            if (!ModelState.IsValid) return View(sliderUpdateVM);
            if (sliderUpdateVM.Photo != null)
            {
                if (!sliderUpdateVM.Photo.ValidatorType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type is incorrect!");
                    return View();
                }
                if (!sliderUpdateVM.Photo.ValidatorSize(FileSize.MB, sliderUpdateVM.Photo.Length))
                {
                    ModelState.AddModelError("Photo", "File size must be less than 2MB!");
                    return View();
                }
                
                string fileName = await sliderUpdateVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");
                sliderUpdateVM.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                slider.ImageUrl = fileName;
            }
            slider.Title = sliderUpdateVM.Title;
            slider.SubTitle = sliderUpdateVM.SubTitle;
            slider.Description = sliderUpdateVM.Description;
            slider.Order = sliderUpdateVM.Order;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}