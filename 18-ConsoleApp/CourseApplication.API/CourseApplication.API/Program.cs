using CourseApplication.Domain.Entities;
using CourseApplication.Repository.Data;
using CourseApplication.Repository.Repositories;
using CourseApplication.Service.Services;

namespace CourseApplication.ConsoleApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Setup (manual DI)
            var db = new InMemoryDbContext();
            var groupRepo = new GroupRepository(db);
            var studentRepo = new StudentRepository(db);
            var groupService = new GroupService(groupRepo);
            var studentService = new StudentService(studentRepo, groupRepo);

            Console.WriteLine("=== Course Application (Console) ===");

            while (true)
            {
                PrintMenu();
                var choice = ReadNonEmpty("Select option").Trim();

                try
                {
                    switch (choice)
                    {
                        case "1": // Create Group
                            var g = ReadGroupInput();
                            var createdG = await groupService.CreateAsync(g);
                            Console.WriteLine($"✔ Group created with Id {createdG.Id}");
                            break;

                        case "2": // Update group
                            await UpdateGroupFlow(groupService);
                            break;

                        case "3": // Delete group
                            await DeleteGroupFlow(groupService, studentService);
                            break;

                        case "4": // Get group by id
                            await GetGroupByIdFlow(groupService);
                            break;

                        case "5": // Get all groups by teacher
                            await GetGroupsByTeacherFlow(groupService);
                            break;

                        case "6": // Get all groups by room
                            await GetGroupsByRoomFlow(groupService);
                            break;

                        case "7": // Get all groups
                            await ShowAllGroups(groupService);
                            break;

                        case "8": // Create Student
                            await CreateStudentFlow(studentService, groupService);
                            break;

                        case "9": // Update Student
                            await UpdateStudentFlow(studentService, groupService);
                            break;

                        case "10": // Get student by id
                            await GetStudentByIdFlow(studentService);
                            break;

                        case "11": // Delete student
                            await DeleteStudentFlow(studentService);
                            break;

                        case "12": // Get students by age
                            await GetStudentsByAgeFlow(studentService);
                            break;

                        case "13": // Get all students by group id
                            await GetStudentsByGroupFlow(studentService);
                            break;

                        case "14": // Search groups by name
                            await SearchGroupsByNameFlow(groupService);
                            break;

                        case "15": // Search students by name or surname
                            await SearchStudentsFlow(studentService);
                            break;

                        case "0":
                            Console.WriteLine("Bye!");
                            return;

                        default:
                            Console.WriteLine("Invalid option — try again.");
                            break;
                    }
                }
                catch (ArgumentException aex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Validation: {aex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1  - Create Group");
            Console.WriteLine("2  - Update Group");
            Console.WriteLine("3  - Delete Group");
            Console.WriteLine("4  - Get Group by Id");
            Console.WriteLine("5  - Get all Groups by Teacher");
            Console.WriteLine("6  - Get all Groups by Room");
            Console.WriteLine("7  - Get all Groups");
            Console.WriteLine("8  - Create Student");
            Console.WriteLine("9  - Update Student");
            Console.WriteLine("10 - Get Student by Id");
            Console.WriteLine("11 - Delete Student");
            Console.WriteLine("12 - Get Students by Age");
            Console.WriteLine("13 - Get all Students by Group Id");
            Console.WriteLine("14 - Search Groups by Name");
            Console.WriteLine("15 - Search Students by Name or Surname");
            Console.WriteLine("0  - Exit");
        }

        #region Flows and helpers

        static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
                Console.WriteLine("Input cannot be empty. Try again.");
            }
        }

        static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var s = Console.ReadLine();
                if (int.TryParse(s, out int v) && v > 0) return v;
                Console.WriteLine("Please enter a positive integer.");
            }
        }

        static Group ReadGroupInput()
        {
            var name = ReadNonEmpty("Group Name");
            var teacher = ReadNonEmpty("Teacher");
            var room = ReadNonEmpty("Room");
            return new Group { Name = name, Teacher = teacher, Room = room };
        }

        static async System.Threading.Tasks.Task UpdateGroupFlow(GroupService groupService)
        {
            int id = ReadPositiveInt("Group Id to update");
            var existing = await groupService.GetByIdAsync(id);
            if (existing == null)
            {
                Console.WriteLine("Group not found.");
                return;
            }

            Console.WriteLine("Leave blank to keep current value.");
            Console.Write($"Name ({existing.Name}): ");
            var n = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(n)) existing.Name = n.Trim();
            Console.Write($"Teacher ({existing.Teacher}): ");
            var t = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(t)) existing.Teacher = t.Trim();
            Console.Write($"Room ({existing.Room}): ");
            var r = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(r)) existing.Room = r.Trim();

            var updated = await groupService.UpdateAsync(existing);
            if (updated == null) Console.WriteLine("Update failed (group not found).");
            else Console.WriteLine("Group updated.");
        }

        static async System.Threading.Tasks.Task DeleteGroupFlow(GroupService groupService, StudentService studentService)
        {
            int id = ReadPositiveInt("Group Id to delete");
            var existing = await groupService.GetByIdAsync(id);
            if (existing == null)
            {
                Console.WriteLine("Group not found.");
                return;
            }

            // Check students in this group
            var students = (await studentService.GetByGroupIdAsync(id)).ToList();
            if (students.Any())
            {
                Console.WriteLine($"Group has {students.Count} student(s). Delete anyway? (yes/no)");
                var ans = Console.ReadLine();
                if (!string.Equals(ans, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Aborted delete.");
                    return;
                }

                // delete students first
                foreach (var s in students)
                    await studentService.DeleteAsync(s.Id);
            }

            await groupService.DeleteAsync(id);
            Console.WriteLine("Group deleted.");
        }

        static async System.Threading.Tasks.Task GetGroupByIdFlow(GroupService groupService)
        {
            int id = ReadPositiveInt("Group Id");
            var g = await groupService.GetByIdAsync(id);
            if (g == null) Console.WriteLine("Group not found.");
            else Console.WriteLine($"[{g.Id}] {g.Name} - {g.Teacher} - {g.Room}");
        }

        static async System.Threading.Tasks.Task GetGroupsByTeacherFlow(GroupService groupService)
        {
            var teacher = ReadNonEmpty("Teacher");
            var groups = (await groupService.GetByTeacherAsync(teacher)).ToList();
            if (!groups.Any()) Console.WriteLine("No groups found for this teacher.");
            else foreach (var gg in groups) Console.WriteLine($"[{gg.Id}] {gg.Name} - {gg.Room}");
        }

        static async System.Threading.Tasks.Task GetGroupsByRoomFlow(GroupService groupService)
        {
            var room = ReadNonEmpty("Room");
            var groups = (await groupService.GetByRoomAsync(room)).ToList();
            if (!groups.Any()) Console.WriteLine("No groups found for this room.");
            else foreach (var gg in groups) Console.WriteLine($"[{gg.Id}] {gg.Name} - Teacher: {gg.Teacher}");
        }

        static async System.Threading.Tasks.Task ShowAllGroups(GroupService groupService)
        {
            var groups = (await groupService.GetAllAsync()).ToList();
            if (!groups.Any()) Console.WriteLine("No groups.");
            else foreach (var g in groups) Console.WriteLine($"[{g.Id}] {g.Name} - {g.Teacher} - {g.Room}");
        }

        static async System.Threading.Tasks.Task CreateStudentFlow(StudentService studentService, GroupService groupService)
        {
            var name = ReadNonEmpty("Student Name");
            var surname = ReadNonEmpty("Student Surname");
            var age = ReadPositiveInt("Age");
            int groupId;
            while (true)
            {
                groupId = ReadPositiveInt("Group Id");
                var g = await groupService.GetByIdAsync(groupId);
                if (g != null) break;
                Console.WriteLine("Group not found. Create a group first or enter another Group Id.");
            }

            var s = new Student { Name = name, Surname = surname, Age = age, GroupId = groupId };
            var created = await studentService.CreateAsync(s);
            Console.WriteLine($"Student created with Id {created.Id}");
        }

        static async System.Threading.Tasks.Task UpdateStudentFlow(StudentService studentService, GroupService groupService)
        {
            int id = ReadPositiveInt("Student Id to update");
            var existing = await studentService.GetByIdAsync(id);
            if (existing == null) { Console.WriteLine("Student not found."); return; }

            Console.WriteLine("Leave blank to keep current value.");
            Console.Write($"Name ({existing.Name}): ");
            var n = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(n)) existing.Name = n.Trim();
            Console.Write($"Surname ({existing.Surname}): ");
            var sur = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(sur)) existing.Surname = sur.Trim();

            Console.Write($"Age ({existing.Age}): ");
            var aIn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(aIn))
            {
                if (int.TryParse(aIn, out var newA) && newA > 0) existing.Age = newA;
                else { Console.WriteLine("Invalid age, keeping previous value."); }
            }

            Console.Write($"GroupId ({existing.GroupId}): ");
            var gIn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(gIn))
            {
                if (int.TryParse(gIn, out var ng) && ng > 0)
                {
                    var g = await groupService.GetByIdAsync(ng);
                    if (g == null) { Console.WriteLine("Group not found, keeping previous GroupId."); }
                    else existing.GroupId = ng;
                }
                else { Console.WriteLine("Invalid GroupId, keeping previous value."); }
            }

            var updated = await studentService.UpdateAsync(existing);
            if (updated == null) Console.WriteLine("Update failed (student not found).");
            else Console.WriteLine("Student updated.");
        }

        static async System.Threading.Tasks.Task GetStudentByIdFlow(StudentService studentService)
        {
            int id = ReadPositiveInt("Student Id");
            var s = await studentService.GetByIdAsync(id);
            if (s == null) Console.WriteLine("Student not found.");
            else Console.WriteLine($"[{s.Id}] {s.Name} {s.Surname} - Age: {s.Age} - GroupId: {s.GroupId}");
        }

        static async System.Threading.Tasks.Task DeleteStudentFlow(StudentService studentService)
        {
            int id = ReadPositiveInt("Student Id to delete");
            var s = await studentService.GetByIdAsync(id);
            if (s == null) { Console.WriteLine("Student not found."); return; }

            Console.Write("Confirm delete student? (yes): ");
            var ans = Console.ReadLine();
            if (!string.Equals(ans, "yes", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("Aborted."); return; }

            await studentService.DeleteAsync(id);
            Console.WriteLine("Student deleted.");
        }

        static async System.Threading.Tasks.Task GetStudentsByAgeFlow(StudentService studentService)
        {
            int age = ReadPositiveInt("Age");
            var list = (await studentService.GetByAgeAsync(age)).ToList();
            if (!list.Any()) Console.WriteLine("No students with that age.");
            else foreach (var s in list) Console.WriteLine($"[{s.Id}] {s.Name} {s.Surname} - GroupId: {s.GroupId}");
        }

        static async System.Threading.Tasks.Task GetStudentsByGroupFlow(StudentService studentService)
        {
            int gid = ReadPositiveInt("Group Id");
            var list = (await studentService.GetByGroupIdAsync(gid)).ToList();
            if (!list.Any()) Console.WriteLine("No students in that group.");
            else foreach (var s in list) Console.WriteLine($"[{s.Id}] {s.Name} {s.Surname} - Age: {s.Age}");
        }

        static async System.Threading.Tasks.Task SearchGroupsByNameFlow(GroupService groupService)
        {
            var term = ReadNonEmpty("Search term for group name");
            var list = (await groupService.SearchByNameAsync(term)).ToList();
            if (!list.Any()) Console.WriteLine("No groups found.");
            else foreach (var g in list) Console.WriteLine($"[{g.Id}] {g.Name} - Teacher: {g.Teacher}, Room: {g.Room}");
        }

        static async System.Threading.Tasks.Task SearchStudentsFlow(StudentService studentService)
        {
            var term = ReadNonEmpty("Search term for student name/surname");
            var list = (await studentService.SearchByNameOrSurnameAsync(term)).ToList();
            if (!list.Any()) Console.WriteLine("No students found.");
            else foreach (var s in list) Console.WriteLine($"[{s.Id}] {s.Name} {s.Surname} - Age: {s.Age} - GroupId: {s.GroupId}");
        }

        #endregion
    }
}
