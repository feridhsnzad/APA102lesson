using System.Collections.Generic;

namespace CourseApplication.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public required string Name { get; set; }           
        public required string Teacher { get; set; }         
        public required string Room { get; set; }            

       
        public List<Student> Students { get; set; } = new();
    }
}
