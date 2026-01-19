namespace FrontToBack.Areas.AdminPanel.ViewModels
{
    public class DetailsProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string TagNames { get; set; }
        public string SizeNames { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
