using CourseApplication.Domain.Entities;

namespace CourseApplication.Repository.Data
{
    public class InMemoryDbContext
    {
        public List<Group> Groups { get; set; } = new();
        public List<Student> Students { get; set; } = new();
    }
}
