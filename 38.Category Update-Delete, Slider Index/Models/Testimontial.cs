namespace FrontToBack.Models
{
    public class Testimontial:BaseEntity
    {
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public int Order { get; set; }
    }
}
