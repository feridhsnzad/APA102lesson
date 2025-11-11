namespace CourseApplication.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Room { get; set; }

        public List<Student> Students { get; set; } = new();
    }
}
