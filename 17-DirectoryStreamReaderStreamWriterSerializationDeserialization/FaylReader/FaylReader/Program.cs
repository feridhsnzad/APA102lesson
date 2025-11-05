using System;
using System.Collections.Generic;
using System.IO;
using StudentSystem;

namespace StudentSystemApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==== Telebe Melumat Sistemi ====\n");

            // 1️⃣ Create students
            List<Student> students = new()
            {
                new Student(1, "Ali", "Memmedov", 20, 85.5),
                new Student(2, "Leyla", "Hesenova", 19, 92.0),
                new Student(3, "Vugar", "Aliyev", 21, 78.5),
                new Student(4, "Nigar", "Ehmedova", 20, 88.0),
                new Student(5, "Reshad", "Quliyev", 22, 95.5)
            };

            Console.WriteLine("Telebeler:");
            foreach (var s in students)
                s.DisplayInfo();

            // 2️⃣ Directory operations
            FileManager fm = new FileManager();

            Console.WriteLine($"\nFolderPath: {fm.FolderPath}");
            Console.WriteLine($"TextFilePath: {fm.TextFilePath}");
            Console.WriteLine($"JsonFilePath: {fm.JsonFilePath}");

            if (fm.CheckFolderExists())
                fm.DeleteFolder();

            fm.CreateFolder();

            Console.WriteLine($"Qovluq movcuddur? {fm.CheckFolderExists()}\n");

            // 3️⃣ Write students
            Console.WriteLine("--- Bir-bir yazma ---");
            foreach (var s in students)
                fm.WriteStudentToFile(s);

            Console.WriteLine("\n--- Toplu yazma ---");
            fm.WriteAllStudentsToFile(students);

            // 4️⃣ Read students
            Console.WriteLine("\n--- Fayldan oxuma ---");
            var readList = fm.ReadStudentsFromFile();
            foreach (var s in readList)
                s.DisplayInfo();

            // 5️⃣ Serialize
            Console.WriteLine("\n--- JSON-a serialize ---");
            fm.SerializeToJson(students);

            // 6️⃣ Deserialize
            Console.WriteLine("\n--- JSON-dan deserialize ---");
            var jsonList = fm.DeserializeFromJson();
            foreach (var s in jsonList)
                s.DisplayInfo();

            // 7️⃣ File contents
            Console.WriteLine("\n--- students.txt fayli ---");
            Console.WriteLine(File.ReadAllText(fm.TextFilePath));

            Console.WriteLine("\n--- students.json fayli ---");
            Console.WriteLine(File.ReadAllText(fm.JsonFilePath));

            // 8️⃣ Statistics
            Console.WriteLine("\n--- Statistikalar ---");
            int count = jsonList.Count;
            double total = 0;
            double max = double.MinValue;
            double min = double.MaxValue;
            int high = 0;

            foreach (var s in jsonList)
            {
                total += s.Grade;
                if (s.Grade > max) max = s.Grade;
                if (s.Grade < min) min = s.Grade;
                if (s.Grade >= 90) high++;
            }

            double avg = total / count;

            Console.WriteLine($"Umumi telebe sayi: {count}");
            Console.WriteLine($"Orta qiymet: {avg:F2}");
            Console.WriteLine($"En yuksek qiymet: {max}");
            Console.WriteLine($"En asagi qiymet: {min}");
            Console.WriteLine($"90+ qiymetli telebe sayi: {high}");

            FileInfo txt = new FileInfo(fm.TextFilePath);
            FileInfo js = new FileInfo(fm.JsonFilePath);

            Console.WriteLine($"\nstudents.txt olcusu: {txt.Length} bayt");
            Console.WriteLine($"students.json olcusu: {js.Length} bayt");

            Console.WriteLine("\n=== Emeliyyatlar ugurla bitdi! ===");
        }
    }
}
