using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Controllers
{
    public class ShopController:Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null ||id< 1 ) return BadRequest();

            Product? product = _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p=>p.Id==id);
            if (product == null) return NotFound();

            DetailVM detailVM = new DetailVM
            {
                Product = product,
            };
            
            return View(detailVM);
        }
    }
}
