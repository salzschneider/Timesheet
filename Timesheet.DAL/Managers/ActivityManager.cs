using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Timesheet;

namespace Timesheet.DAL.Managers
{
    public class ActivityManager
    {
        public static void AddActivityItem(string title, string description)
        {
            using (var db = new TimesheetEntities())
            {
                var activityItem = new Activities()
                {
                    Title       = title,
                    Description = description,
                };

                db.Activities.Add(activityItem);
                db.SaveChanges();
            }
        }

        public static List<Activities> GetAllActivities()
        {
            var allActivityList = new List<Activities>();

            using (var db = new TimesheetEntities())
            {
                var queryResult = db.Activities.OrderBy(c => c.Id);

                allActivityList = queryResult.ToList();
            }

            return allActivityList;
        }
    }
}
