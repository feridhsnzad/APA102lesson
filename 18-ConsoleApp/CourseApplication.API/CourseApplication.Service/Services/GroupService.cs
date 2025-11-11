using CourseApplication.Domain.Entities;
using CourseApplication.Domain.Interfaces;

namespace CourseApplication.Service.Services
{
    public class GroupService
    {
        private readonly IGroupRepository _repo;
        public GroupService(IGroupRepository repo) => _repo = repo;

        public Task<Group> CreateAsync(Group group) => _repo.AddAsync(group);
        public Task<Group> UpdateAsync(Group group) => _repo.UpdateAsync(group);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
        public Task<Group> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<IEnumerable<Group>> GetAllAsync() => _repo.GetAllAsync();
        public Task<IEnumerable<Group>> GetByTeacherAsync(string teacher) => _repo.GetByTeacherAsync(teacher);
        public Task<IEnumerable<Group>> GetByRoomAsync(string room) => _repo.GetByRoomAsync(room);
        public Task<IEnumerable<Group>> SearchByNameAsync(string name) => _repo.SearchByNameAsync(name);
    }
}
