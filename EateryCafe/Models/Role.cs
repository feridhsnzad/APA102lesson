namespace EateryCafe.Models
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        public List<Chef> Chefs { get; set; }
    }
}
