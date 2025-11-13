using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApplication.Domain.Entities;
using CourseApplication.Domain.Interfaces;
using CourseApplication.Repository.Data;

namespace CourseApplication.Repository.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly InMemoryDbContext _db;
        private int _nextId = 1;
        public GroupRepository(InMemoryDbContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

        public Task<Group> AddAsync(Group entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Id = _nextId++;
            _db.Groups.Add(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(int id)
        {
            var g = _db.Groups.FirstOrDefault(x => x.Id == id);
            if (g != null) _db.Groups.Remove(g);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Group>> GetAllAsync()
            => Task.FromResult<IEnumerable<Group>>(_db.Groups.ToList());

        public Task<Group> GetByIdAsync(int id)
        {
            return Task.FromResult(_db.Groups.FirstOrDefault(x => x.Id == id));
        }

        public Task<Group> UpdateAsync(Group entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var g = _db.Groups.FirstOrDefault(x => x.Id == entity.Id);
            if (g == null) return Task.FromResult<Group>(null);

            g.Name = entity.Name;
            g.Teacher = entity.Teacher;
            g.Room = entity.Room;
            return Task.FromResult(g);
        }

        public Task<IEnumerable<Group>> GetByTeacherAsync(string teacher)
            => Task.FromResult<IEnumerable<Group>>(_db.Groups.Where(x => string.Equals(x.Teacher, teacher, StringComparison.OrdinalIgnoreCase)).ToList());

        public Task<IEnumerable<Group>> GetByRoomAsync(string room)
            => Task.FromResult<IEnumerable<Group>>(_db.Groups.Where(x => string.Equals(x.Room, room, StringComparison.OrdinalIgnoreCase)).ToList());

        public Task<IEnumerable<Group>> SearchByNameAsync(string namePart)
            => Task.FromResult<IEnumerable<Group>>(_db.Groups.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.IndexOf(namePart ?? "", StringComparison.OrdinalIgnoreCase) >= 0).ToList());
    }
}
