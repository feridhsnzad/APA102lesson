using System.Collections.Generic;
using CourseApplication.Domain.Entities;

namespace CourseApplication.Repository.Data
{
   
    public class InMemoryDbContext
    {
        public List<Group> Groups { get; } = new();
        public List<Student> Students { get; } = new();
    }
}
