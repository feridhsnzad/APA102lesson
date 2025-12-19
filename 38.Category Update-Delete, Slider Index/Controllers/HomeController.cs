using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FrontToBack.DAL;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context= context;
        }
  
        //List<Testimontial> testimontials = new List<Testimontial>
        //{
        //    new Testimontial{Name="Basliq 1",Occupation="Client",Description="Hamiya Salam 1",UrlImage="1--1rnu4p.png",Order=1, IsDeleted=false, CreatedAt=DateTime.Now },
        //    new Testimontial{Name="Basliq 1",Occupation="Client",Description="Hamiya Salam 1",UrlImage="2.png",Order=2, IsDeleted=false, CreatedAt=DateTime.Now },
        //    new Testimontial{Name="Basliq 1",Occupation="Client",Description="Hamiya Salam 1",UrlImage="3.png",Order=3, IsDeleted=false, CreatedAt=DateTime.Now },

        //};

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.OrderBy(s => s.Order).ToListAsync();
            List<Shipping> shippings = await _context.Shippings.ToListAsync();
            List<Testimontial> testimontials = await _context.Testimontials.OrderBy(s => s.Order).ToListAsync();
            List<Product> products = await _context.Products.Include(p=>p.ProductImages.Where(pi=>pi.IsPrimary != null)).ToListAsync();

            //Product product = _context.Products.Include(p=>p.Category).FirstOrDefault();

            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Shippings = shippings,
                Testimontials = testimontials,
                Products = products
            };

            return View(homeVM);
        }
    }
}   

