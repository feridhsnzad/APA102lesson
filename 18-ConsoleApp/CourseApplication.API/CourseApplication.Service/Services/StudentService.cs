using CourseApplication.Domain.Entities;
using CourseApplication.Domain.Interfaces;

namespace CourseApplication.Service.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo) => _repo = repo;

        public Task<Student> CreateAsync(Student s) => _repo.AddAsync(s);
        public Task<Student> UpdateAsync(Student s) => _repo.UpdateAsync(s);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
        public Task<Student> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<IEnumerable<Student>> GetByAgeAsync(int age) => _repo.GetByAgeAsync(age);
        public Task<IEnumerable<Student>> GetByGroupIdAsync(int groupId) => _repo.GetByGroupIdAsync(groupId);
        public Task<IEnumerable<Student>> SearchByNameOrSurnameAsync(string term) => _repo.SearchByNameOrSurnameAsync(term);
        public Task<IEnumerable<Student>> GetAllAsync() => _repo.GetAllAsync();
    }
}
