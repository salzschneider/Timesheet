using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Core.DTO;

namespace Timesheet.BLL.Services
{
    public interface IUserService
    {
        void Add(UsersFullDTO usersFullDTO);

        IEnumerable<UsersFullDTO> GetAll();

        Task<IEnumerable<UsersFullDTO>> GetAllAsync();

        UsersFullDTO GetById(int id);
    }
}