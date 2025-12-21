using Microsoft.AspNetCore.Mvc;

namespace Dinamic_Property
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //app.MapControllerRoute(
            //    "Coporativ",
            //    "korporativ-satislar",
            //    new { controller = "home", action = "corporativesales" }
            //    );
            app.MapControllerRoute(
                name: "defaolt",
                pattern: "{controller=home}/{action=index}/{id?}"
                );

            app.Run();
        }
    }
}
