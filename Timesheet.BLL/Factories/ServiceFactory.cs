using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.BLL.Services;
using Timesheet.DAL.UnitOfWork;
using Timesheet.DAL.Timesheet;


namespace Timesheet.BLL.Factories
{
    public static class ServiceFactory
    {
        public static IActivityService CreateActivityService()
        {
            return new ActivityService(new TimesheetUnitOfWork(new TimesheetEntities()));
        }

        public static IUserService CreateUserService()
        {
            return new UserService(new TimesheetUnitOfWork(new TimesheetEntities()));
        }

        public static IUserActivityService CreateUserActivityService()
        {
            return new UserActivityService(new TimesheetUnitOfWork(new TimesheetEntities()));
        }
    }
}
