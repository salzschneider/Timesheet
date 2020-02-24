using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Core.DTO;

namespace Timesheet.BLL.Services
{
    public interface IUserActivityService
    {
        void Add(UserActivitiesFullDTO userActivitiesFullDTO);

        IEnumerable<UserActivitiesExtendedDTO> GetExtendedAll();

        Task<IEnumerable<UserActivitiesExtendedDTO>> GetExtendedAllAsync();

        IEnumerable<UserActivitiesAggrByActivityDTO> GetSumDurationByActivities(DateTime startDate, DateTime endDate, int userId = 0);

        Task<IEnumerable<UserActivitiesAggrByActivityDTO>> GetSumDurationByActivitiesAsync(DateTime startDate, DateTime endDate, int userId = 0);
    }
}