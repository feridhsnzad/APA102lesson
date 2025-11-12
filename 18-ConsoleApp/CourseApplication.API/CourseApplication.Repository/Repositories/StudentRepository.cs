using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApplication.Domain.Entities;
using CourseApplication.Domain.Interfaces;
using CourseApplication.Repository.Data;

namespace CourseApplication.Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly InMemoryDbContext _db;
        private int _nextId = 1;
        public StudentRepository(InMemoryDbContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

        public Task<Student> AddAsync(Student entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Id = _nextId++;
            _db.Students.Add(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(int id)
        {
            var s = _db.Students.FirstOrDefault(x => x.Id == id);
            if (s != null) _db.Students.Remove(s);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Student>> GetAllAsync()
            => Task.FromResult<IEnumerable<Student>>(_db.Students.ToList());

        public Task<Student> GetByIdAsync(int id)
            => Task.FromResult(_db.Students.FirstOrDefault(x => x.Id == id));

        public Task<Student> UpdateAsync(Student entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var s = _db.Students.FirstOrDefault(x => x.Id == entity.Id);
            if (s == null) return Task.FromResult<Student>(null);

            s.Name = entity.Name;
            s.Surname = entity.Surname;
            s.Age = entity.Age;
            s.GroupId = entity.GroupId;
            return Task.FromResult(s);
        }

        public Task<IEnumerable<Student>> GetByAgeAsync(int age)
            => Task.FromResult<IEnumerable<Student>>(_db.Students.Where(x => x.Age == age).ToList());

        public Task<IEnumerable<Student>> GetByGroupIdAsync(int groupId)
            => Task.FromResult<IEnumerable<Student>>(_db.Students.Where(x => x.GroupId == groupId).ToList());

        public Task<IEnumerable<Student>> SearchByNameOrSurnameAsync(string term)
            => Task.FromResult<IEnumerable<Student>>(_db.Students.Where(x =>
                (!string.IsNullOrEmpty(x.Name) && x.Name.IndexOf(term ?? "", StringComparison.OrdinalIgnoreCase) >= 0) ||
                (!string.IsNullOrEmpty(x.Surname) && x.Surname.IndexOf(term ?? "", StringComparison.OrdinalIgnoreCase) >= 0)
            ).ToList());
    }
}
