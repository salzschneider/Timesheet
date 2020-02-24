using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Core.DTO;
using Timesheet.DAL.Timesheet;

namespace Timesheet.BLL.Services
{
    public interface IActivityService
    {
        /// <summary>
        /// Add new activity
        /// </summary>
        /// <param name="activityFullDTO"></param>
        void Add(ActivitiesFullDTO activityDTO);

        /// <summary>
        /// Get all activities
        /// </summary>
        /// <returns></returns>
        IEnumerable<ActivitiesFullDTO> GetAll();

        /// <summary>
        /// Get all activities
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task<IEnumerable<ActivitiesFullDTO>> GetAllAsync();
    }
}