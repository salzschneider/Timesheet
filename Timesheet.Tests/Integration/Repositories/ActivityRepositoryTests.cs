using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Timesheet.DAL.Repositories;
using Timesheet.DAL.Timesheet;
using Timesheet.Test.Integration.Fixtures;
using Timesheet.Test.Integration.Collections;


namespace Timesheet.Test.Integration.Repositories
{
    [Collection(nameof(TimesheetDatabaseCollection))]
    public class ActivityRepositoryTests
    {
        private TimesheetDatabaseFixture dbFixture;
        private List<Activities> baseActivitiesList;
        private ActivityRepository activityRepo;


        public ActivityRepositoryTests(TimesheetDatabaseFixture dbInputFixture)
        {
            dbFixture = dbInputFixture;
            baseActivitiesList = dbFixture.ActivitiesInitData.ToList();
            dbFixture.RebuildActivitiesTable(dbFixture.CreateDbContext());
            activityRepo = new ActivityRepository(dbFixture.CurrentDbContext);
        }
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void GetById_BaseDataElements_ReturnSameUserDataFromDatabaseById(int actualIndex, int expectedIndex)
        {
            //arrange
            var expected = baseActivitiesList[expectedIndex];

            //act
            var actual = activityRepo.GetById(baseActivitiesList[actualIndex].Id);

            Assert.True(actual.Id == expected.Id &&
                        actual.Description == expected.Description &&
                        actual.Title == expected.Title);
        }

        [Fact]
        public void GetById_InvalidIndex_ReturnNull()
        {
            //act
            var actual = activityRepo.GetById(-1);

            Assert.Null(actual);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async void GetByIdAsync_BaseDataElements_ReturnSameUserDataFromDatabaseById(int actualIndex, int expectedIndex)
        {
            //arrange
            var expected = baseActivitiesList[expectedIndex];

            //act
            var actual = await activityRepo.GetByIdAsync(baseActivitiesList[actualIndex].Id);

            Assert.True(actual.Id == expected.Id &&
                        actual.Description == expected.Description &&
                        actual.Title == expected.Title);
        }

        [Fact]
        public async void GetByIdAsync_InvalidIndex_ReturnNull()
        {
            //act
            var actual = await activityRepo.GetByIdAsync(-1);

            Assert.Null(actual);
        }

        [Fact]
        public void GetAll_InitDataElements_ReturnSameListFromDatabase()
        {
            //arrange
            var expectedList = baseActivitiesList;

            //act
            var actualList = activityRepo.GetAll().ToList();

            //assert
            Assert.True(IsEqualActivtiesLists(expectedList, actualList));
        }

        [Fact]
        public async void GetAllAsync_InitDataElements_ReturnSameListFromDatabase()
        {
            //arrange
            var expectedList = baseActivitiesList;

            //act
            var actualList = await activityRepo.GetAllAsync();

            //assert
            Assert.True(IsEqualActivtiesLists(expectedList, actualList.ToList()));
        }

        private bool IsEqualActivtiesLists(List<Activities> actualList, List<Activities> expectedList)
        {
            bool isStillEqual = actualList.Count == expectedList.Count;

            if (isStillEqual)
            {
                for (int i = 0; i < actualList.Count; i++)
                {
                    if (actualList[i].Id != expectedList[i].Id ||
                        actualList[i].Description != expectedList[i].Description ||
                        actualList[i].Title != expectedList[i].Title)
                    {
                        isStillEqual = false;
                        break;
                    }
                }
            }

            return isStillEqual;
        }

        [Fact]
        public void Add_NewElementToBase_ReturnBaseListPlusNewElement()
        {
            //arrange
            var expectedList = baseActivitiesList.ToList();
            Activities newElement = new Activities() { Id = 4, Title = "New Title", Description = "New Description"};
            expectedList.Add(newElement);
            
            //activityRepo = new ActivityRepository(dbFixture.CreateDbContext());

            //act
            activityRepo.Add(newElement);
            dbFixture.CurrentDbContext.SaveChanges();
            var actualList = activityRepo.GetAll().ToList();

            //assert
            Assert.True(IsEqualActivtiesLists(actualList, expectedList));
        }
        
        [Fact]
        public void Add_NewElementToBase_NOTEqualLists()
        {
            //arrange
            var expectedList = baseActivitiesList.ToList();
            Activities newElement = new Activities() { Id = 4, Title = "New Title 2", Description = "New Description 2" };
            Activities badApple = new Activities() { Title = "New Title 3", Description = "New Description 3" };
            expectedList.Add(newElement);

            //act
            activityRepo.Add(badApple);
            dbFixture.CurrentDbContext.SaveChanges();
            var actualList = activityRepo.GetAll().ToList();

            //assert
            Assert.False(IsEqualActivtiesLists(actualList, expectedList));
        }
    }
}
