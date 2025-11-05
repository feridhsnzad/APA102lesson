using System;

namespace StudentSystem
{
    [Serializable]
    public class Student
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }

        // Constructor
        public Student(int id, string name, string surname, int age, double grade)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            Grade = grade;
        }

        // Show student info
        public void DisplayInfo()
        {
            Console.WriteLine($"[{Id}] {Name} {Surname} - Yas: {Age}, Qiymet: {Grade}");
        }

        // ToString for file writing (CSV format)
        public override string ToString()
        {
            return $"{Id},{Name},{Surname},{Age},{Grade}";
        }
    }
}
