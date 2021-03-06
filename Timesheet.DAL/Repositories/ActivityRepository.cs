﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Core.DTO;
using Timesheet.DAL.Timesheet;

namespace Timesheet.DAL.Repositories
{
    public class ActivityRepository : Repository<Activities>, IActivityRepository
    {
        public TimesheetEntities TimesheetEntities
        {
            get { return DbContext as TimesheetEntities; }
        }

        public ActivityRepository(TimesheetEntities dbContext) : base(dbContext)
        {

        }
    }
}
