using System.ComponentModel.DataAnnotations.Schema;

namespace FrontToBack.Areas.AdminPanel.ViewModels
{
    public class SliderCreateVM
    {
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IFormFile Photo { get; set; }

    }
}
