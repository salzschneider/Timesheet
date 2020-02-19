using System.Collections.Generic;
using Timesheet.Core.DTO;
using Timesheet.DAL.Timesheet;

namespace Timesheet.BLL.Services
{
    public interface IActivityService
    {
        void Add(ActivitiesFullDTO activityDTO);

        List<ActivitiesFullDTO> GetAll();
    }
}