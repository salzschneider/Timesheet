using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Timesheet;
using System.Data.Entity;

namespace Timesheet.DAL.Managers
{
    public class UserActivityManager
    {
        public static void AddUserActivityItem(int userId, int activityId, int duration, string comment, System.DateTime dateTime)
        {
            using (var db = new TimesheetEntities())
            {
                var userActivityItem = new UserActivities()
                {
                    UserId     = userId,
                    ActivityId = activityId,
                    Duration   = duration,
                    Comment    = comment,
                    Date       = dateTime,
                };

                db.UserActivities.Add(userActivityItem);
                db.SaveChanges();
            }
        }

        public static List<UserActivitiesDTO> GetAllUserActivities()
        {
            var allUserActivityList = new List<UserActivitiesDTO>();

            using (var db = new TimesheetEntities())
            {
                allUserActivityList = db.UserActivities
                    .Select(userActivity => new UserActivitiesDTO
                    {
                        Id = userActivity.Id,
                        UserId = userActivity.UserId,
                        Username = db.Users.FirstOrDefault(u => u.Id == userActivity.UserId).Username,
                        ActivityId = userActivity.ActivityId,
                        ActivityName = db.Activities.FirstOrDefault(a => a.Id == userActivity.ActivityId).Title,
                        Duration = userActivity.Duration,
                        Comment = userActivity.Comment,
                        Date = userActivity.Date,

                    }).OrderBy(c => c.Id).ToList();
            }

            return allUserActivityList;
        }

        /// <summary>
        /// Getting activities and the related duration sum
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<UserActivitiesDTO> GetSumDurationByActivities(DateTime startDate, DateTime endDate)
        {
            var activityList = new List<UserActivitiesDTO>();

            using (var db = new TimesheetEntities())
            {
                var queryResult = db.UserActivities
                     .Where(x => (DbFunctions.TruncateTime(x.Date) >= DbFunctions.TruncateTime(startDate)) && (DbFunctions.TruncateTime(x.Date) <= DbFunctions.TruncateTime(endDate)))
                     .GroupBy(ua => ua.ActivityId)
                     .Select(g => new UserActivitiesDTO
                     {
                         ActivityId = g.Key,
                         ActivityName = db.Activities.FirstOrDefault(a => a.Id == g.Key).Title,
                         SumDuration = g.Sum(ua => ua.Duration),
                     });

                activityList = queryResult.ToList();
            }

            return activityList;
        }

        /// <summary>
        /// Getting duration sum of activities related to a concrete user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<UserActivitiesDTO> GetUserActivitiesDuration(int userId, DateTime startDate, DateTime endDate)
        {
            var activityList = new List<UserActivitiesDTO>();

            using (var db = new TimesheetEntities())
            {
                var queryResult = db.UserActivities
                     .Where(x => (DbFunctions.TruncateTime(x.Date) >= DbFunctions.TruncateTime(startDate)) && (DbFunctions.TruncateTime(x.Date) <= DbFunctions.TruncateTime(endDate)))
                     .Where(x => x.UserId == userId )
                     .GroupBy(ua => ua.ActivityId)
                     .Select(g => new UserActivitiesDTO
                     {
                         ActivityId = g.Key,
                         ActivityName = db.Activities.FirstOrDefault(a => a.Id == g.Key).Title,
                         SumDuration = g.Sum(ua => ua.Duration),
                     });

                activityList = queryResult.ToList();
            }

            return activityList;
        }


    }
}
