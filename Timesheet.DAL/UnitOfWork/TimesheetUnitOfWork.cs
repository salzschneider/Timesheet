using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Repositories;
using Timesheet.DAL.Timesheet;

namespace Timesheet.DAL.UnitOfWork
{
    public class TimesheetUnitOfWork : UnitOfWork, ITimesheetUnitOfWork
    {
        public TimesheetEntities TimesheetEntities
        {
            get { return DbContext as TimesheetEntities; }
        }

        public TimesheetUnitOfWork(TimesheetEntities dbContext) : base(dbContext)
        {
            Activity = new ActivityRepository(dbContext);
            User = new UserRepository(dbContext);
            UserActivity = new UserActivityRepository(dbContext);
        }

        public IActivityRepository Activity { get; private set; }

        public IUserRepository User { get; private set; }

        public IUserActivityRepository UserActivity { get; private set; }
    }
}
