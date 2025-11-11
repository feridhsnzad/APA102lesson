using CourseApplication.Domain.Entities;
using CourseApplication.Domain.Interfaces;
using CourseApplication.Repository.Data;

namespace CourseApplication.Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly InMemoryDbContext _context;
        private int _idCounter = 1;

        public StudentRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        public Task<Student> AddAsync(Student entity)
        {
            entity.Id = _idCounter++;
            _context.Students.Add(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(int id)
        {
            var s = _context.Students.FirstOrDefault(x => x.Id == id);
            if (s != null)
                _context.Students.Remove(s);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Student>> GetAllAsync()
            => Task.FromResult(_context.Students.AsEnumerable());

        public Task<Student> GetByIdAsync(int id)
            => Task.FromResult(_context.Students.FirstOrDefault(x => x.Id == id));

        public Task<Student> UpdateAsync(Student entity)
        {
            var existing = _context.Students.FirstOrDefault(x => x.Id == entity.Id);
            if (existing != null)
            {
                existing.Name = entity.Name;
                existing.Surname = entity.Surname;
                existing.Age = entity.Age;
                existing.GroupId = entity.GroupId;
            }
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Student>> GetByAgeAsync(int age)
            => Task.FromResult(_context.Students.Where(x => x.Age == age).AsEnumerable());

        public Task<IEnumerable<Student>> GetByGroupIdAsync(int groupId)
            => Task.FromResult(_context.Students.Where(x => x.GroupId == groupId).AsEnumerable());

        public Task<IEnumerable<Student>> SearchByNameOrSurnameAsync(string term)
            => Task.FromResult(_context.Students.Where(x =>
                x.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                x.Surname.Contains(term, StringComparison.OrdinalIgnoreCase)).AsEnumerable());
    }
}
