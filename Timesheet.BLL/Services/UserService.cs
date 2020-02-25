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
            var dllList = await timesheetUnitOfWork.User.GetAllAsync();

            return dllList.Select(user => new UsersFullDTO
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                FullName = user.FullName,

            });
        }

        /// <summary>
        /// Get an entry with the given id. If no entry is found, the null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UsersFullDTO GetById(int id)
        {
            var user = timesheetUnitOfWork.User.GetById(id);

            if (user is null) throw new NullReferenceException("User object can't be null. Invalid user id was given.");

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
