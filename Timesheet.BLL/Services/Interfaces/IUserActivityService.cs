using System;
using System.Collections.Generic;
using Timesheet.Core.DTO;

namespace Timesheet.BLL.Services
{
    public interface IUserActivityService
    {
        void Add(UserActivitiesFullDTO userActivitiesFullDTO);

        List<UserActivitiesExtendedDTO> GetExtendedAll();

        List<UserActivitiesAggrByActivityDTO> GetSumDurationByActivities(DateTime startDate, DateTime endDate, int userId = 0);
    }
}