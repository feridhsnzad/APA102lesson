namespace FrontToBack.Areas.AdminPanel.ViewModels
{
    public class SliderUpdateVM
    {
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
