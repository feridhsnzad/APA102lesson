using FrontToBack.Areas.AdminPanel.ViewModels;
using FrontToBack.DAL;
using FrontToBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,Moderator, Member")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetCategoryVM> getCategoryVMs = await _context.Categories
                .Where(c => c.IsDeleted == false)
                .Include(c => c.Products)
                .Select(c => new GetCategoryVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductCount = c.Products.Count
                })
                .ToListAsync();

            return View(getCategoryVMs);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Create(CreateCategoryVM createCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existCategory = await _context.Categories.AnyAsync(c => c.Name.Trim() == createCategoryVM.Name.Trim());

            if (existCategory)
            {
                ModelState.AddModelError("Name", "Bu adda category artiq movcuddur");
                return View();
            }
            Category category = new()
            {
                Name = createCategoryVM.Name
            };
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            UpdateCategoryVM updateCategoryVM = new()
            {
                Name = category.Name
            };

            if (category is null) return NotFound();
            return View(updateCategoryVM);
        }




        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Update(int? id, UpdateCategoryVM updateCategoryVM)
        {
            if (id is null || id < 1) return BadRequest();

            Category? existscategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (updateCategoryVM is null) return NotFound();

            if (!ModelState.IsValid) return View();

            bool result = await _context.Categories.AnyAsync(c => c.Name.Trim() == updateCategoryVM.Name.Trim() && c.Id != id);
            if (result)
            {
                ModelState.AddModelError(nameof(Category.Name), "Bu adda category artiq movcuddur");
                return View();
            }

            existscategory.Name = updateCategoryVM.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Category? existscategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existscategory is null) return NotFound();

            existscategory.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Category? category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
            if (category is null) return NotFound();

            DetailsCategoryVM detailsCategoryVM = new()
            {
                Id = category.Id,
                Name = category.Name,
                ProductCount = category.Products.Count,
                CreatedAt = category.CreatedAt
            };

            return View(detailsCategoryVM);
        }
    }
}
