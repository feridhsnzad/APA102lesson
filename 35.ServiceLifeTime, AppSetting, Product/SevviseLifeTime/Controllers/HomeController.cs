using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Controllers
{
    public class HomeController: Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        //List<Slider> sliders = new List<Slider>
        //{
        //    new Slider{ SubTitle="Komekci Basliq 1",Title="basliq 1", Description="gullerden qalmadi ", ImageURL="1-2-524x617.png", Order=2, IsDeleted=false, CreatedAt=DateTime.Now},
        //    new Slider{ SubTitle="Komekci Basliq 2",Title="basliq 2", Description="mohtesenm endirm  ", ImageURL="1-1-524x617.png", Order=3, IsDeleted=false, CreatedAt=DateTime.Now},
        //    new Slider{ SubTitle="Komekci Basliq 3",Title="basliq 3", Description="xirdalana manatdan ", ImageURL="indir.jpeg", Order=1, IsDeleted=false, CreatedAt=DateTime.Now}

        //};
        //public IActionResult Test()
        //{
        //    _context.Sliders.AddRange(sliders);
        //    _context.SaveChanges();
        //    return Ok();
        //}

        public async Task<IActionResult> Index()
        {
            List<Slider>sliders=await _context.Sliders
                .OrderBy(s=>s.Order)
                .ToListAsync();
            List<Product> products = await _context.Products
                .Include(p=>p.ProductImages.Where(pi=>pi.IsPrimary !=null))
                .ToListAsync();
            //List<Shipping> shippings = await _context.Shippings
              //.ToListAsync();
            //List<Slider> sliders = _context.Sliders.Where(s => !s.IsDeleted).ToList();
            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Products = products,
            };

            return View(homeVM);
        }
    }
}
