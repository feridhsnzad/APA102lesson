using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApplication.Domain.Entities;

namespace CourseApplication.Domain.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetByAgeAsync(int age);
        Task<IEnumerable<Student>> GetByGroupIdAsync(int groupId);
        Task<IEnumerable<Student>> SearchByNameOrSurnameAsync(string term);
    }
}
