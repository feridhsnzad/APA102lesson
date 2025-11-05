using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace StudentSystem
{
    public class FileManager
    {
        public string FolderPath { get; set; }
        public string TextFilePath { get; set; }
        public string JsonFilePath { get; set; }

        // Constructor
        public FileManager()
        {
            string baseDir = Directory.GetCurrentDirectory();
            FolderPath = Path.Combine(baseDir, "StudentData");
            TextFilePath = Path.Combine(FolderPath, "students.txt");
            JsonFilePath = Path.Combine(FolderPath, "students.json");
        }

        // Create folder
        public void CreateFolder()
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
                Console.WriteLine($"Qovluq yaradildi: {FolderPath}");
            }
            else
            {
                Console.WriteLine($"Qovluq artiq movcuddur: {FolderPath}");
            }
        }

        // Delete folder
        public void DeleteFolder()
        {
            if (Directory.Exists(FolderPath))
            {
                Directory.Delete(FolderPath, true);
                Console.WriteLine($"Qovluq silindi: {FolderPath}");
            }
        }

        // Check if folder exists
        public bool CheckFolderExists()
        {
            return Directory.Exists(FolderPath);
        }

        // Write single student
        public void WriteStudentToFile(Student student)
        {
            using (StreamWriter sw = new StreamWriter(TextFilePath, append: true))
            {
                sw.WriteLine(student.ToString());
            }
            Console.WriteLine($"Telebe fayla yazildi: {student.Name}");
        }

        // Write all students
        public void WriteAllStudentsToFile(List<Student> students)
        {
            File.WriteAllText(TextFilePath, string.Empty);

            using (StreamWriter sw = new StreamWriter(TextFilePath))
            {
                foreach (var s in students)
                    sw.WriteLine(s.ToString());
            }

            Console.WriteLine($"Umumi {students.Count} telebe fayla yazildi");
        }

        // Read from file
        public List<Student> ReadStudentsFromFile()
        {
            List<Student> students = new();

            if (!File.Exists(TextFilePath))
            {
                Console.WriteLine($"Fayl tapilmadi: {TextFilePath}");
                return students;
            }

            using (StreamReader sr = new StreamReader(TextFilePath))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 5)
                    {
                        students.Add(new Student(
                            int.Parse(parts[0]),
                            parts[1],
                            parts[2],
                            int.Parse(parts[3]),
                            double.Parse(parts[4])
                        ));
                    }
                }
            }

            Console.WriteLine($"Fayldan {students.Count} telebe oxundu");
            return students;
        }

        // Serialize to JSON
        public void SerializeToJson(List<Student> students)
        {
            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonFilePath, json);
            Console.WriteLine($"Telebeler JSON formatinda saxlanildi: {JsonFilePath}");
        }

        // Deserialize from JSON
        public List<Student> DeserializeFromJson()
        {
            List<Student> students = new();

            if (!File.Exists(JsonFilePath))
            {
                Console.WriteLine($"JSON fayli tapilmadi: {JsonFilePath}");
                return students;
            }

            string json = File.ReadAllText(JsonFilePath);
            students = JsonSerializer.Deserialize<List<Student>>(json);
            Console.WriteLine($"JSON-dan {students.Count} telebe yuklendi");
            return students;
        }
    }
}
