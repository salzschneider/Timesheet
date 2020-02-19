using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Timesheet.Core.DTO;
using Timesheet.DAL.Timesheet;

namespace Timesheet.DAL.Repositories
{
    public interface IUserActivityRepository : IRepository<UserActivities>
    {
        IEnumerable<UserActivitiesExtendedDTO> GetExtendedAll();

        IEnumerable<UserActivitiesAggrByActivityDTO> GetSumDurationByActivities(DateTime startDate, DateTime endDate, int userId = 0);
    }
}
