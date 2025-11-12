using System.Collections.Generic;

namespace CourseApplication.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }           // required
        public string Teacher { get; set; }        // required
        public string Room { get; set; }           // required

        // NOTE: For in-memory simplicity we won't automatically maintain navigation collection,
        // but it's here to respect domain model.
        public List<Student> Students { get; set; } = new();
    }
}
