using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Core.DTO;
using Timesheet.DAL.Timesheet;
using Timesheet.DAL.UnitOfWork;

namespace Timesheet.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ITimesheetUnitOfWork timesheetUnitOfWork;

        public UserService(ITimesheetUnitOfWork timesheetUnitOfWork)
        {
            this.timesheetUnitOfWork = timesheetUnitOfWork;
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="userFullDTO"></param>
        public void Add(UsersFullDTO usersFullDTO)
        {
            timesheetUnitOfWork.User.Add(new Users()
            {
                Username = usersFullDTO.Username,
                Password = usersFullDTO.Password,
                FullName = usersFullDTO.FullName,
            });

            timesheetUnitOfWork.Commit();
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UsersFullDTO> GetAll()
        {
            return timesheetUnitOfWork.User.GetAll()
                .Select(user => new UsersFullDTO
                {
                    Id       = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    FullName = user.FullName,

                });
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UsersFullDTO>> GetAllAsync()
        {
            return await Task<IEnumerable<UsersFullDTO>>.Run(() => GetAll());
        }

        public UsersFullDTO GetById(int id)
        {
            var user = timesheetUnitOfWork.User.GetById(id);

            return new UsersFullDTO()
            {
                Id       = user.Id,
                Username = user.Username,
                Password = user.Password,
                FullName = user.FullName,
            };
        }
    }
}
