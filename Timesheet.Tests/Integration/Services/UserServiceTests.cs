using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Timesheet.BLL.Services;
using Timesheet.DAL.Repositories;
using Timesheet.DAL.Timesheet;
using Timesheet.DAL.UnitOfWork;
using Timesheet.Test.Integration.Collections;
using Timesheet.Test.Integration.Fixtures;


namespace Timesheet.Test.Integration.Services
{
    [Collection(nameof(TimesheetDatabaseCollection))]
    public class UserServiceTests
    {
        private TimesheetDatabaseFixture dbFixture;
        private UserService userService;
        private List<Users> baseUsersList;

        public UserServiceTests(TimesheetDatabaseFixture dbInputFixture)
        {
            dbFixture = dbInputFixture;
            baseUsersList = dbFixture.UsersInitData.ToList();

            dbFixture.CleanUserActivitiesTable(dbFixture.CreateDbContext());
            dbFixture.RebuildUsersTable(dbFixture.CurrentDbContext);

            userService = new UserService(new TimesheetUnitOfWork(dbFixture.CurrentDbContext));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetById_BaseUsersList_ReturnSameUserDataFromDatabaseById(int baseUserIndex)
        {
            //arrange
            var expected = baseUsersList[baseUserIndex];

            //act
            var actual = userService.GetById(baseUsersList[baseUserIndex].Id);

            Assert.True(actual.Id              == expected.Id &&
                        actual.Username        == expected.Username &&
                        actual.Password.Trim() == expected.Password &&
                        actual.FullName        == expected.FullName );
        }

        [Fact]
        public void GetById_InvalidIndex_ThrowException()
        {
            //assert
            Assert.Throws<NullReferenceException>(() => userService.GetById(-1));
        }
    }
}
