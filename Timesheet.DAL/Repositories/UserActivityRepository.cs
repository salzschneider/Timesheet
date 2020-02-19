using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Core.DTO;
using Timesheet.DAL.Timesheet;

namespace Timesheet.DAL.Repositories
{
    public class UserActivityRepository : Repository<UserActivities>, IUserActivityRepository
    {
        public TimesheetEntities TimesheetEntities
        {
            get { return DbContext as TimesheetEntities; }
        }

        public UserActivityRepository(TimesheetEntities dbContext) : base(dbContext)
        {

        }

        /// <summary>
        /// Get all user activities with additional fields (ActivityName, Username)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserActivitiesExtendedDTO> GetExtendedAll()
        {
            var allUserActivityList = TimesheetEntities.UserActivities
                     .Select(userActivity => new UserActivitiesExtendedDTO
                     {
                         Id           = userActivity.Id,
                         UserId       = userActivity.UserId,
                         Username     = TimesheetEntities.Users.FirstOrDefault(u => u.Id == userActivity.UserId).Username,
                         ActivityId   = userActivity.ActivityId,
                         ActivityName = TimesheetEntities.Activities.FirstOrDefault(a => a.Id == userActivity.ActivityId).Title,
                         Duration     = userActivity.Duration,
                         Comment      = userActivity.Comment,
                         Date         = userActivity.Date,
                     }).OrderBy(c => c.Id);

            return allUserActivityList.AsEnumerable();
        }

        /// <summary>
        /// Getting activities and the related duration sum. Filtering by user id is optional. 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="userId">Filtering the result by user id. If this argument is 0, the result won't be filtered.</param>
        /// <returns></returns>
        public IEnumerable<UserActivitiesAggrByActivityDTO> GetSumDurationByActivities(DateTime startDate, DateTime endDate, int userId)
        {
            var activityList = TimesheetEntities.UserActivities
                     .Where(x => (DbFunctions.TruncateTime(x.Date) >= DbFunctions.TruncateTime(startDate)) && (DbFunctions.TruncateTime(x.Date) <= DbFunctions.TruncateTime(endDate)))
                     .Where(x => (userId != 0 && x.UserId == userId) || userId == 0)
                     .GroupBy(ua => ua.ActivityId)
                     .Select(g => new UserActivitiesAggrByActivityDTO
                     {
                         ActivityId   = g.Key,
                         ActivityName = TimesheetEntities.Activities.FirstOrDefault(a => a.Id == g.Key).Title,
                         SumDuration  = g.Sum(ua => ua.Duration),
                     }).OrderBy(c => c.ActivityName);

            return activityList.AsEnumerable();
        }

    }
}
