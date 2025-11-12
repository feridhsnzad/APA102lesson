using System.Collections.Generic;
using CourseApplication.Domain.Entities;

namespace CourseApplication.Repository.Data
{
    // Simple in-memory "db"
    public class InMemoryDbContext
    {
        public List<Group> Groups { get; } = new();
        public List<Student> Students { get; } = new();
    }
}
