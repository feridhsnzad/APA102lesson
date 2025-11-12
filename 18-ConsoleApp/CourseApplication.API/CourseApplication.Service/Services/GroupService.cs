using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApplication.Domain.Entities;
using CourseApplication.Domain.Interfaces;

namespace CourseApplication.Service.Services
{
    public class GroupService
    {
        private readonly IGroupRepository _repo;
        public GroupService(IGroupRepository repo) => _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public Task<Group> CreateAsync(Group group)
        {
            ValidateGroupForCreate(group);
            return _repo.AddAsync(group);
        }

        public async Task<Group> UpdateAsync(Group group)
        {
            ValidateGroupForUpdate(group);
            var existing = await _repo.GetByIdAsync(group.Id);
            if (existing == null) return null;
            return await _repo.UpdateAsync(group);
        }

        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

        public Task<Group> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public Task<IEnumerable<Group>> GetAllAsync() => _repo.GetAllAsync();

        public Task<IEnumerable<Group>> GetByTeacherAsync(string teacher)
        {
            if (string.IsNullOrWhiteSpace(teacher)) throw new ArgumentException("Teacher is required.");
            return _repo.GetByTeacherAsync(teacher);
        }

        public Task<IEnumerable<Group>> GetByRoomAsync(string room)
        {
            if (string.IsNullOrWhiteSpace(room)) throw new ArgumentException("Room is required.");
            return _repo.GetByRoomAsync(room);
        }

        public Task<IEnumerable<Group>> SearchByNameAsync(string namePart)
        {
            if (string.IsNullOrWhiteSpace(namePart)) throw new ArgumentException("Search term is required.");
            return _repo.SearchByNameAsync(namePart);
        }

        private void ValidateGroupForCreate(Group g)
        {
            if (g == null) throw new ArgumentNullException(nameof(g));
            if (string.IsNullOrWhiteSpace(g.Name)) throw new ArgumentException("Group name is required.");
            if (string.IsNullOrWhiteSpace(g.Teacher)) throw new ArgumentException("Teacher is required.");
            if (string.IsNullOrWhiteSpace(g.Room)) throw new ArgumentException("Room is required.");
        }

        private void ValidateGroupForUpdate(Group g)
        {
            if (g == null) throw new ArgumentNullException(nameof(g));
            if (g.Id <= 0) throw new ArgumentException("Valid group Id is required for update.");
            ValidateGroupForCreate(g);
        }
    }
}
