using CourseApplication.Domain.Entities;
using CourseApplication.Repository.Data;
using CourseApplication.Repository.Repositories;
using CourseApplication.Service.Services;

var db = new InMemoryDbContext();
var groupRepo = new GroupRepository(db);
var studentRepo = new StudentRepository(db);

var groupService = new GroupService(groupRepo);
var studentService = new StudentService(studentRepo);

while (true)
{
    Console.WriteLine("\n=== COURSE APPLICATION ===");
    Console.WriteLine("1. Create Group");
    Console.WriteLine("2. Update Group");
    Console.WriteLine("3. Delete Group");
    Console.WriteLine("4. Get Group by Id");
    Console.WriteLine("5. Get Groups by Teacher");
    Console.WriteLine("6. Get Groups by Room");
    Console.WriteLine("7. Get All Groups");
    Console.WriteLine("8. Create Student");
    Console.WriteLine("9. Update Student");
    Console.WriteLine("10. Get Student by Id");
    Console.WriteLine("11. Delete Student");
    Console.WriteLine("12. Get Students by Age");
    Console.WriteLine("13. Get Students by Group Id");
    Console.WriteLine("14. Search Groups by Name");
    Console.WriteLine("15. Search Students by Name or Surname");
    Console.WriteLine("0. Exit");
    Console.Write("Select: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Group name: ");
            var name = Console.ReadLine();
            Console.Write("Teacher: ");
            var teacher = Console.ReadLine();
            Console.Write("Room: ");
            var room = Console.ReadLine();
            await groupService.CreateAsync(new Group { Name = name, Teacher = teacher, Room = room });
            Console.WriteLine("Group created.");
            break;

        case "7":
            var all = await groupService.GetAllAsync();
            foreach (var g in all)
                Console.WriteLine($"[{g.Id}] {g.Name} - {g.Teacher} - {g.Room}");
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}
