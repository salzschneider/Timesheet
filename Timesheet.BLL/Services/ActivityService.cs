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

        public void Add(ActivitiesFullDTO activityFullDTO)
        {
            timesheetUnitOfWork.Activity.Add(new Activities()
            {
                Title       = activityFullDTO.Title,
                Description = activityFullDTO.Description,
            });

            timesheetUnitOfWork.Commit();
        }

        
        public IEnumerable<ActivitiesFullDTO> GetAll()
        {
            return timesheetUnitOfWork.Activity.GetAll()
            .Select(activity => new ActivitiesFullDTO
            {
                Id          = activity.Id,
                Title       = activity.Title,
                Description = activity.Description,

            });
        }

        public async Task<IEnumerable<ActivitiesFullDTO>> GetAllAsync()
        {
            return await Task<IEnumerable<ActivitiesFullDTO>>.Run(() => GetAll());
        }
    }
}
