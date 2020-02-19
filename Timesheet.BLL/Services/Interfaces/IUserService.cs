using System.Collections.Generic;
using Timesheet.Core.DTO;

namespace Timesheet.BLL.Services
{
    public interface IUserService
    {
        void Add(UsersFullDTO usersFullDTO);

        List<UsersFullDTO> GetAll();

        UsersFullDTO GetById(int id);
    }
}