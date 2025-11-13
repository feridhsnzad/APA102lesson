using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApplication.Domain.Entities;
using CourseApplication.Domain.Interfaces;

namespace CourseApplication.Service.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repo;
        private readonly IGroupRepository _groupRepo;

        public StudentService(IStudentRepository repo, IGroupRepository groupRepo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _groupRepo = groupRepo ?? throw new ArgumentNullException(nameof(groupRepo));
        }

        public async Task<Student> CreateAsync(Student s)
        {
            ValidateStudentForCreate(s);
           
            var grp = await _groupRepo.GetByIdAsync(s.GroupId);
            if (grp == null) throw new ArgumentException($"Group with Id {s.GroupId} does not exist.");
            return await _repo.AddAsync(s);
        }

        public async Task<Student?> UpdateAsync(Student s)
        {
            ValidateStudentForUpdate(s);
            var exists = await _repo.GetByIdAsync(s.Id);
            if (exists == null) return null;

            var grp = await _groupRepo.GetByIdAsync(s.GroupId);
            if (grp == null) throw new ArgumentException($"Group with Id {s.GroupId} does not exist.");

            return await _repo.UpdateAsync(s);
        }

        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

        public Task<Student> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public Task<IEnumerable<Student>> GetAllAsync() => _repo.GetAllAsync();

        public Task<IEnumerable<Student>> GetByAgeAsync(int age)
        {
            if (age <= 0) throw new ArgumentException("Age must be positive.");
            return _repo.GetByAgeAsync(age);
        }

        public Task<IEnumerable<Student>> GetByGroupIdAsync(int groupId)
        {
            if (groupId <= 0) throw new ArgumentException("GroupId must be positive.");
            return _repo.GetByGroupIdAsync(groupId);
        }

        public Task<IEnumerable<Student>> SearchByNameOrSurnameAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) throw new ArgumentException("Search term is required.");
            return _repo.SearchByNameOrSurnameAsync(term);
        }

        private void ValidateStudentForCreate(Student s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));
            if (string.IsNullOrWhiteSpace(s.Name)) throw new ArgumentException("Student name is required.");
            if (string.IsNullOrWhiteSpace(s.Surname)) throw new ArgumentException("Student surname is required.");
            if (s.Age <= 0) throw new ArgumentException("Student age must be > 0.");
            if (s.GroupId <= 0) throw new ArgumentException("Valid GroupId is required.");
        }

        private void ValidateStudentForUpdate(Student s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));
            if (s.Id <= 0) throw new ArgumentException("Valid Student Id is required for update.");
            ValidateStudentForCreate(s);
        }
    }
}
