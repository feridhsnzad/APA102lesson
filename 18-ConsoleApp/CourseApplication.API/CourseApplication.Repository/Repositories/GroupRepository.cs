using CourseApplication.Domain.Entities;
using CourseApplication.Domain.Interfaces;
using CourseApplication.Repository.Data;
using System.Text.RegularExpressions;

namespace CourseApplication.Repository.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly InMemoryDbContext _context;
        private int _idCounter = 1;

        public GroupRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        public Task<Group> AddAsync(Group entity)
        {
            entity.Id = _idCounter++;
            _context.Groups.Add(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(int id)
        {
            var g = _context.Groups.FirstOrDefault(x => x.Id == id);
            if (g != null)
                _context.Groups.Remove(g);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Group>> GetAllAsync()
            => Task.FromResult(_context.Groups.AsEnumerable());

        public Task<Group> GetByIdAsync(int id)
            => Task.FromResult(_context.Groups.FirstOrDefault(x => x.Id == id));

        public Task<Group> UpdateAsync(Group entity)
        {
            var existing = _context.Groups.FirstOrDefault(x => x.Id == entity.Id);
            if (existing != null)
            {
                existing.Name = entity.Name;
                existing.Teacher = entity.Teacher;
                existing.Room = entity.Room;
            }
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Group>> GetByTeacherAsync(string teacher)
            => Task.FromResult(_context.Groups.Where(x => x.Teacher == teacher).AsEnumerable());

        public Task<IEnumerable<Group>> GetByRoomAsync(string room)
            => Task.FromResult(_context.Groups.Where(x => x.Room == room).AsEnumerable());

        public Task<IEnumerable<Group>> SearchByNameAsync(string name)
            => Task.FromResult(_context.Groups.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).AsEnumerable());
    }
}
