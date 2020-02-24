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

        Task<IEnumerable<UserActivitiesExtendedDTO>> GetExtendedAllAsync();

        IEnumerable<UserActivitiesAggrByActivityDTO> GetSumDurationByActivities(DateTime startDate, DateTime endDate, int userId = 0);

        Task<IEnumerable<UserActivitiesAggrByActivityDTO>> GetSumDurationByActivitiesAsync(DateTime startDate, DateTime endDate, int userId);
    }
}
