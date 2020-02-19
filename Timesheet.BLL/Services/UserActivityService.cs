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
    public class UserActivityService : IUserActivityService
    {
        private readonly ITimesheetUnitOfWork timesheetUnitOfWork;

        public UserActivityService(ITimesheetUnitOfWork timesheetUnitOfWork)
        {
            this.timesheetUnitOfWork = timesheetUnitOfWork;
        }

        /// <summary>
        /// Add new user activity
        /// </summary>
        /// <param name="userActivitiesFullDTO"></param>
        public void Add(UserActivitiesFullDTO userActivitiesFullDTO)
        {
            timesheetUnitOfWork.UserActivity.Add(new UserActivities()
            {
                UserId     = userActivitiesFullDTO.UserId,
                ActivityId = userActivitiesFullDTO.ActivityId,
                Duration   = userActivitiesFullDTO.Duration,
                Comment    = userActivitiesFullDTO.Comment,
                Date       = userActivitiesFullDTO.Date,
            });

            timesheetUnitOfWork.Commit();
        }

        /// <summary>
        /// Get all user activity with additional fields - (ActivityName, Username)
        /// </summary>
        /// <returns></returns>
        public List<UserActivitiesExtendedDTO> GetExtendedAll()
        {
            return timesheetUnitOfWork.UserActivity.GetExtendedAll().ToList();
        }


        /// <summary>
        /// Getting activities and the related duration sum. Filtering by user id is optional. 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="userId">Filtering the result by user id. If this argument is 0 the result won't be filtered.</param>
        /// <returns></returns>
        public List<UserActivitiesAggrByActivityDTO> GetSumDurationByActivities(DateTime startDate, DateTime endDate, int userId = 0)
        {
            return timesheetUnitOfWork.UserActivity.GetSumDurationByActivities(startDate, endDate, userId).ToList();
        }
    }
}
