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
    public class ActivityService : IActivityService
    {
        private readonly ITimesheetUnitOfWork timesheetUnitOfWork;

        public ActivityService(ITimesheetUnitOfWork timesheetUnitOfWork)
        {
            this.timesheetUnitOfWork = timesheetUnitOfWork;
        }

        /// <summary>
        /// Add new activity
        /// </summary>
        /// <param name="activityFullDTO"></param>
        public void Add(ActivitiesFullDTO activityFullDTO)
        {
            timesheetUnitOfWork.Activity.Add(new Activities()
            {
                Title       = activityFullDTO.Title,
                Description = activityFullDTO.Description,
            });

            timesheetUnitOfWork.Commit();
        }

        /// <summary>
        /// Get all activities
        /// </summary>
        /// <returns></returns>
        public List<ActivitiesFullDTO> GetAll()
        {
            return timesheetUnitOfWork.Activity.GetAll()
            .Select(activity => new ActivitiesFullDTO
            {
                Id          = activity.Id,
                Title       = activity.Title,
                Description = activity.Description,

            }).ToList();
        }
    }
}
