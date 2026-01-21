namespace EateryCafe.Models
{
    public class Chef: BaseEntity
    {
        public string Name { get; set; }
        public string Describtion { get; set; }
        public string Image { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
