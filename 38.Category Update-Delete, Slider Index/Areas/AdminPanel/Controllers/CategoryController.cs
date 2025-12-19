using FrontToBack.DAL;
using FrontToBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace FrontToBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Categories
                .Include(c => c.Products)
                .Where(c => c.IsDeleted == false)
                .ToListAsync();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (category == null) return BadRequest();

            if (!ModelState.IsValid) return View(category);

            bool isExist = await _context.Categories
                .AnyAsync(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower());

            if (isExist)
            {
                ModelState.AddModelError("Name", "Category already exists!");
                return View(category);
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null || id < 1) return BadRequest();
            if (category == null) return BadRequest();

            Category existedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existedCategory == null) return NotFound();

            if (!ModelState.IsValid) return View(category);

            
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                ModelState.AddModelError("Name", "Name cannot be empty!");
                return View(category);
            }

            bool isDuplicate = await _context.Categories
                .AnyAsync(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower() && c.Id != id);

            if (isDuplicate)
            {
                ModelState.AddModelError("Name", "Category already exists!");
                return View(category);
            }

            existedCategory.Name = category.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Category existCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existCategory == null) return NotFound();

            if (existCategory.IsDeleted = false)
            {
                existCategory.IsDeleted = true;
            }
            else
            {
                existCategory.IsDeleted = false;
            }

            existCategory.IsDeleted = true;

            // _context.Categories.Remove(existCategory);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Category category = _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

            if (category is null) return NotFound();

            return View(category);
        }
    }
}