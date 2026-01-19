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

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<GetProductVM> products = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Select(p => new GetProductVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ProductImages.FirstOrDefault(pi=>pi.IsPrimary == true).ImageUrl
                })
                .ToListAsync();
            return View(products);
        }
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Product? product = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
                .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            DetailsProductVM detailsProductVM = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                TagNames = string.Join(", ", product.ProductTags.Select(pt => pt.Tag.Name)),
                SizeNames = string.Join(", ", product.ProductSizes.Select(ps => ps.Size.Name)),
                CategoryName = product.Category.Name,
                ImageUrl = product.ProductImages.FirstOrDefault(pi => pi.IsPrimary == true).ImageUrl
            };
            if (product == null) return NotFound();

            return View(detailsProductVM);
        }
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Create()
        {
            CreateProductVM createProductVM = new CreateProductVM
            {
                Sizes = await _context.Sizes.ToListAsync(),
                Tags = await _context.Tags.ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };
            return View(createProductVM);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Create(CreateProductVM createProductVM)
        {
            createProductVM.Categories = await _context.Categories.ToListAsync();
            createProductVM.Tags = await _context.Tags.ToListAsync();
            createProductVM.Sizes = await _context.Sizes.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(createProductVM);
            }
            if(!createProductVM.MainPhoto.ValidatorType("image/"))
            {
                ModelState.AddModelError(nameof(CreateProductVM.MainPhoto), "File type is incorrect");
                return View(createProductVM);
            }
            if (!createProductVM.MainPhoto.ValidatorSize(FileSize.MB, 1))
            {
                ModelState.AddModelError(nameof(CreateProductVM.MainPhoto), "File size must be less than 1mb!");
                return View(createProductVM);
            }
            if (!createProductVM.HoverPhoto.ValidatorType("image/"))
            {
                ModelState.AddModelError(nameof(CreateProductVM.HoverPhoto), "File type is incorrect");
                return View(createProductVM);
            }
            if (!createProductVM.HoverPhoto.ValidatorSize(FileSize.MB, 1))
            {
                ModelState.AddModelError(nameof(CreateProductVM.HoverPhoto), "File size must be less than 1mb!");
                return View(createProductVM);
            }



            bool existscategory = _context.Categories.Any(c => c.Id == createProductVM.CategoryId);
            if (!existscategory)
            {
                ModelState.AddModelError(nameof(CreateProductVM.CategoryId), "Category does not exists");
                return View(createProductVM);
            }

            if (createProductVM.TagIds != null)
            {

                bool exsisTag = createProductVM.TagIds.Any(tagId => createProductVM.Tags.Exists(t => t.Id == tagId));

                if (!exsisTag)
                {
                    ModelState.AddModelError(nameof(CreateProductVM.TagIds), "Tag does not exists");
                    return View(createProductVM);
                }

            }
            if (createProductVM.SizeIds != null)
            {

                bool existsSize = createProductVM.SizeIds.Any(sizeId => createProductVM.Sizes.Exists(s => s.Id == sizeId));

                if (!existsSize)
                {
                    ModelState.AddModelError(nameof(CreateProductVM.SizeIds), "Size does not exists");
                    return View(createProductVM);
                }

            }

            ProductImage mainImage = new()
            {
                ImageUrl = await createProductVM.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images"),
                IsPrimary = true
            };
            ProductImage hoverImage = new()
            {
                ImageUrl = await createProductVM.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images"),
                IsPrimary = false
            };


            Product newProduct = new Product
            {
                Name = createProductVM.Name,
                Price = createProductVM.Price,
                Description = createProductVM.description,
                SKU = createProductVM.SKU,
                CategoryId = createProductVM.CategoryId.Value,
                ProductImages = new List<ProductImage> { mainImage,hoverImage}
            };

            if (createProductVM.TagIds is not null)
            {
                newProduct.ProductTags = createProductVM.TagIds.Select(tId => new ProductTag { TagId = tId }).ToList();
            }
            if (createProductVM.SizeIds is not null)
            {
                newProduct.ProductSizes = createProductVM.SizeIds.Select(sId => new ProductSize { SizeId = sId }).ToList();
            }
            if (createProductVM.AdditionalPhotos is not null) 
            {
                string text = string.Empty;
                foreach (IFormFile file in createProductVM.AdditionalPhotos)
                {
                    if (!file.ValidatorType("image/"))
                    {
                        text += $"<p class=\"text-danger\"></p>{file.FileName} type was not correct ";
                        continue;
                    }
                    if (!file.ValidatorSize(FileSize.MB, 1))
                    {
                        text += $"<p class=\"text-danger\"></p>{file.FileName} size was not correct ";
                        continue;
                    }
                    newProduct.ProductImages.Add(new ProductImage
                    {
                        ImageUrl = await file.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images"),
                        IsPrimary = null
                    });
                }
                TempData["FileWarning"] = text;
            }

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View();
            }

            Product? existedproduct = await _context.Products
                .Include(pi=>pi.ProductImages)
                .Include(p => p.ProductTags)
                .Include(ps=>ps.ProductSizes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existedproduct is null) return NotFound();

            UpdateProductVM updateProductVM = new()
            {
                Name = existedproduct.Name,
                Price = existedproduct.Price,
                description = existedproduct.Description,
                SKU = existedproduct.SKU,
                CategoryId = existedproduct.CategoryId,
                TagIds = existedproduct.ProductTags.Select(pt => pt.TagId).ToList(),
                SizeIds = existedproduct.ProductSizes.Select(ps => ps.SizeId).ToList(),
                Categories = await _context.Categories.ToListAsync(),
                Sizes = await _context.Sizes.ToListAsync(),
                Tags = await _context.Tags.ToListAsync(),
                ProductImages = existedproduct.ProductImages,
            };
            return View(updateProductVM);

        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Update(int? id, UpdateProductVM updateProductVM)
        {
            if (id is null || id < 1) return BadRequest();
            Product? existedproduct = await _context.Products
                .Include(pi => pi.ProductImages)
                .Include(pt => pt.ProductTags)
                .Include(ps => ps.ProductSizes)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (existedproduct is null) return NotFound();

            updateProductVM.Categories = await _context.Categories.ToListAsync();
            updateProductVM.Tags = await _context.Tags.ToListAsync();
            updateProductVM.ProductImages = existedproduct.ProductImages;
            updateProductVM.Sizes = await _context.Sizes.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(updateProductVM);
            }



            if(updateProductVM.MainPhoto is not null)
            {
                if (!updateProductVM.MainPhoto.ValidatorType("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateProductVM.MainPhoto), "File type is incorrect");
                    return View(updateProductVM);
                }
                if (!updateProductVM.MainPhoto.ValidatorSize(FileSize.MB, 1))
                {
                    ModelState.AddModelError(nameof(UpdateProductVM.MainPhoto), "File size must be less than 1mb!");
                    return View(updateProductVM);
                }
            }

            if (updateProductVM.HoverPhoto is not null)
            {
                if (!updateProductVM.HoverPhoto.ValidatorType("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateProductVM.HoverPhoto), "File type is incorrect");
                    return View(updateProductVM);
                }
                if (!updateProductVM.HoverPhoto.ValidatorSize(FileSize.MB, 1))
                {
                    ModelState.AddModelError(nameof(UpdateProductVM.HoverPhoto), "File size must be less than 1mb!");
                    return View(updateProductVM);
                }
            }

            bool existscategory = updateProductVM.Categories.Any(c => c.Id == updateProductVM.CategoryId);
            if (!existscategory)
            {
                return View(updateProductVM);
            }


            if (updateProductVM.TagIds != null)
            {

                bool exsisTag = updateProductVM.TagIds.Any(tagId => updateProductVM.Tags.Exists(t => t.Id == tagId));

                if (!exsisTag)
                {
                    ModelState.AddModelError(nameof(UpdateProductVM.TagIds), "Tag does not exists");
                    return View(updateProductVM);
                }

            }

            if (updateProductVM.TagIds is null)
            {
                updateProductVM.TagIds = new();
            }
            else
            {
                updateProductVM.TagIds = updateProductVM.TagIds.Distinct().ToList();
            }

            if (updateProductVM.TagIds is not null) 
            {
                List<ProductTag> deletedTags = existedproduct.ProductTags.
                Where(pTag => !updateProductVM.TagIds
                .Exists(tId => tId == pTag.TagId))
                .ToList();


                List<ProductTag> createdTags = updateProductVM.TagIds.Where(tId => !existedproduct.ProductTags
                    .Exists(pTag => pTag.TagId == tId))
                    .Select(tId => new ProductTag { TagId = tId, ProductId = existedproduct.Id })
                    .ToList();


                _context.ProductTags.RemoveRange(deletedTags);
                _context.ProductTags.AddRange(createdTags);
            }

            if (updateProductVM.SizeIds != null)
            {

                bool exsisSize = updateProductVM.SizeIds.Any(sizeId => updateProductVM.Sizes.Exists(t => t.Id == sizeId));

                if (!exsisSize)
                {
                    ModelState.AddModelError(nameof(UpdateProductVM.SizeIds), "Size does not exists");
                    return View(updateProductVM);
                }

            }
            if (updateProductVM.SizeIds is null)
            {
                updateProductVM.SizeIds = new();
            }
            else
            {
                updateProductVM.SizeIds = updateProductVM.SizeIds.Distinct().ToList();
            }

            if (updateProductVM.SizeIds is not null)
            {
                List<ProductSize> deletedSizes = existedproduct.ProductSizes.
                Where(pSize => !updateProductVM.SizeIds
                .Exists(sId => sId == pSize.SizeId))
                .ToList();


                List<ProductSize> createdSizes = updateProductVM.SizeIds.Where(sId => !existedproduct.ProductSizes
                    .Exists(pSize => pSize.SizeId == sId))
                    .Select(sId => new ProductSize { SizeId = sId, ProductId = existedproduct.Id })
                    .ToList();


                _context.ProductSizes.RemoveRange(deletedSizes);
                _context.ProductSizes.AddRange(createdSizes);
            }
            if (updateProductVM.MainPhoto is not null) 
            {
                string fileName = await updateProductVM.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");

               ProductImage mainImage = existedproduct.ProductImages.FirstOrDefault(pi =>pi.IsPrimary == true);
                mainImage.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                existedproduct.ProductImages.Remove(mainImage);
                existedproduct.ProductImages.Add(new ProductImage
                {
                    ImageUrl = fileName,
                    IsPrimary = true
                });
            }
            if (updateProductVM.HoverPhoto is not null)
            {
                string fileName = await updateProductVM.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");

                ProductImage hoverImage = existedproduct.ProductImages.FirstOrDefault(pi => pi.IsPrimary == false);
                hoverImage.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                existedproduct.ProductImages.Remove(hoverImage);
                existedproduct.ProductImages.Add(new ProductImage
                {
                    ImageUrl = fileName,
                    IsPrimary = false
                });
            }
            if (updateProductVM.ImageIds is null)
            {
                    updateProductVM.ImageIds = new List<int>();
            }

            var deletedImages = existedproduct.ProductImages
                .Where(pi => !updateProductVM.ImageIds.Exists(imgId => imgId == pi.Id) && pi.IsPrimary == null)
                .ToList();
            deletedImages.ForEach(di =>di.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images", "website-images"));
            _context.ProductImages.RemoveRange(deletedImages);
            if (updateProductVM.AdditionalPhotos is not null)
            {
                string text = string.Empty;
                foreach (IFormFile file in updateProductVM.AdditionalPhotos)
                {
                    if (!file.ValidatorType("image/"))
                    {
                        text += $"<p class=\"text-danger\"></p>{file.FileName} type was not correct ";
                        continue;
                    }
                    if (!file.ValidatorSize(FileSize.MB, 1))
                    {
                        text += $"<p class=\"text-danger\"></p>{file.FileName} size was not correct ";
                        continue;
                    }
                    existedproduct.ProductImages.Add(new ProductImage
                    {
                        ImageUrl = await file.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images"),
                        IsPrimary = null
                    });
                }
                TempData["FileWarning"] = text;
            }

            existedproduct.Name = updateProductVM.Name;
            existedproduct.Price = updateProductVM.Price;
            existedproduct.Description = updateProductVM.description;
            existedproduct.SKU = updateProductVM.SKU;
            existedproduct.CategoryId = updateProductVM.CategoryId.Value;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Product existedProduct = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductTags)
                .Include(p => p.ProductSizes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existedProduct is null) return NotFound();

            foreach (var image in existedProduct.ProductImages)
            {
                image.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
            }


            _context.ProductImages.RemoveRange(existedProduct.ProductImages);
            _context.ProductTags.RemoveRange(existedProduct.ProductTags);
            _context.ProductSizes.RemoveRange(existedProduct.ProductSizes);


            _context.Products.Remove(existedProduct);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}