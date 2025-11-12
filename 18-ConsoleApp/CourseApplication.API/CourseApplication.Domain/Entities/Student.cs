namespace CourseApplication.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }       // required
        public string Surname { get; set; }    // required
        public int Age { get; set; }           // required, > 0
        public int GroupId { get; set; }       // required, must refer existing Group
    }
}
