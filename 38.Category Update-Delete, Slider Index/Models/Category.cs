using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace FrontToBack.Models
{
    public class Category:BaseEntity
    {
        //[Required(ErrorMessage ="Bos yazmaq olmaz!!!")]
        [MaxLength(30, ErrorMessage ="Agilli ol inspektden artirma")]
        public string? Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
