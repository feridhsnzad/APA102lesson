using FrontToBack.Models;
using System.ComponentModel.DataAnnotations;

namespace FrontToBack.Areas.AdminPanel.ViewModels
{
    public class CreateProductVM
    {
        public IFormFile MainPhoto { get; set; }
        public IFormFile HoverPhoto { get; set; }
        public List<IFormFile>? AdditionalPhotos { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string description { get; set; }
        public string SKU { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        public List<int>? TagIds { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<int>? SizeIds { get; set; }
        public List<Size>? Sizes { get; set; }
    }
}
