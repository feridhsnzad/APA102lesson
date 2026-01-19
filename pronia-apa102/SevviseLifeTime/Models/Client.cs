namespace FrontToBack.Models
{
    public class Client:BaseEntity
    {
        public string ImageURl {  get; set; }
        public string UserNAme { get; set; }
        public string UserOccupation { get; set; }
        public string UserComment { get; set; }
        public int Order {  get; set; }
    }
}
