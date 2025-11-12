using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApplication.Domain.Entities;

namespace CourseApplication.Domain.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<IEnumerable<Group>> GetByTeacherAsync(string teacher);
        Task<IEnumerable<Group>> GetByRoomAsync(string room);
        Task<IEnumerable<Group>> SearchByNameAsync(string namePart);
    }
}
