using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Timesheet.Test.Integration.Fixtures;

namespace Timesheet.Test.Integration.Collections
{
    [CollectionDefinition(nameof(TimesheetDatabaseCollection), DisableParallelization = true)]
    public class TimesheetDatabaseCollection : ICollectionFixture<TimesheetDatabaseFixture>
    {

    }
}
