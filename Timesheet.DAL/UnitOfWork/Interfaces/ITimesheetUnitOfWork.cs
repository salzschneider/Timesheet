using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Repositories;

namespace Timesheet.DAL.UnitOfWork
{
    public interface ITimesheetUnitOfWork : IUnitOfWork
    {
        IActivityRepository Activity { get; }

        IUserRepository User { get; }

        IUserActivityRepository UserActivity { get; }
    }
}

