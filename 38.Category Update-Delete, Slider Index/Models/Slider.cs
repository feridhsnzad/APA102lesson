using System.ComponentModel.DataAnnotations.Schema;

namespace FrontToBack.Models
{
    public class Slider:BaseEntity
    {
        public string Subtitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
