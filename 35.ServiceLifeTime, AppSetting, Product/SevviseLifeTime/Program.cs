using FrontToBack.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt=>
     opt.UseSqlServer(builder.Configuration.GetConnectionString("Defoult"))
    );

var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
    "admin",
    "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    "default",
    "{controller=home}/{action=Index}/{id?}"
    );
  
app.Run();
