using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Locators;
using Timesheet.DAL.Timesheet;
using System.Data.Entity;

namespace Timesheet.Test.Integration.Fixtures
{
    public class TimesheetDatabaseFixture : IDisposable
    {
        private TimesheetDbContextLocator dbContextLocator;

        public TimesheetEntities CurrentDbContext { get => dbContextLocator.Current; }

        public List<Activities> ActivitiesInitData { get;  }

        public List<UserActivities> UserActivitiesInitData { get;  }

        public List<Users> UsersInitData { get; private set; }


        /// <summary>
        /// Creating a new dbContext and setting it as current. 
        /// </summary>
        /// <returns></returns>
        public TimesheetEntities CreateDbContext()
        {
            dbContextLocator.Reset();
            return CurrentDbContext;
        }

        public TimesheetDatabaseFixture()
        {
            dbContextLocator = new TimesheetDbContextLocator();

            ActivitiesInitData = new List<Activities>()
            {
                new Activities(){ Id = 1, Title = "Meeting", Description = "Simple Meeting"},
                new Activities(){ Id = 2, Title = "QS Implementation", Description = "Hard coding work" },
                new Activities(){ Id = 3, Title = "Administration", Description = ""},
            };

            UsersInitData = new List<Users>()
            {
                new Users(){ Id = 1, Username = "Admin", Password = "Plaintext now 1", FullName = "Joe Smith"},
                new Users(){ Id = 2, Username = "Peter1", Password = "Plaintext now 2", FullName = "Peter Taylor"},
                new Users(){ Id = 3, Username = "Ann12", Password = "Plaintext now 3", FullName = "Annabelle Anderson"},
            };

            UserActivitiesInitData = new List<UserActivities>()
            {
                new UserActivities(){ Id = 1, UserId = 1, ActivityId = 1, Duration = 3650, Comment = "Sprint planning meeting", Date = new DateTime(2020, 2, 25, 9, 30, 00)},
                new UserActivities(){ Id = 2, UserId = 1, ActivityId = 2, Duration = 13650, Comment = "Working like hell", Date = new DateTime(2020, 2, 26, 9, 30, 00)},
                new UserActivities(){ Id = 3, UserId = 2, ActivityId = 3, Duration = 3650, Comment = "Writing documentation", Date = new DateTime(2020, 2, 25, 9, 30, 00)},
            };
        }

        public void RebuildDatabaseTables()
        {
            using (var dbCont = new TimesheetEntities())
            {
                //deleting dependecies
                CleanUserActivitiesTable(dbCont);
                CleanActivitiesTable(dbCont);
                CleanUsersTable(dbCont);

                dbCont.Activities.AddRange(ActivitiesInitData);
                dbCont.Users.AddRange(UsersInitData);
                dbCont.UserActivities.AddRange(UserActivitiesInitData);
                dbCont.SaveChanges();
            }
        }

        public void RebuildActivitiesTable(TimesheetEntities dbContext)
        {
            //deleting dependecies
            CleanActivitiesTable(dbContext);

            dbContext.Activities.AddRange(ActivitiesInitData);
            dbContext.SaveChanges();
        }

        public void RebuildUsersTable(TimesheetEntities dbContext)
        {
            //deleting dependecies
            CleanUsersTable(dbContext);

            dbContext.Users.AddRange(UsersInitData);
            dbContext.SaveChanges();
        }

        public void RebuildUsersActivitiesTable(TimesheetEntities dbContext)
        {
            //deleting dependecies
            CleanActivitiesTable(dbContext);

            dbContext.UserActivities.AddRange(UserActivitiesInitData);
            dbContext.SaveChanges();
        }

        public void CleanUserActivitiesTable(DbContext db)
        {
            db.Database.ExecuteSqlCommand("DELETE FROM [UserActivities]");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('UserActivities', RESEED, 0)");
        }

        public void CleanActivitiesTable(DbContext db)
        {
            db.Database.ExecuteSqlCommand("DELETE FROM [Activities]");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Activities', RESEED, 0)");
        }

        public void CleanUsersTable(DbContext db)
        {
            db.Database.ExecuteSqlCommand("DELETE FROM [Users]");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Users', RESEED, 0)");
        }

        //At the and of the database related tests, tables have to be rebuilt
        public void Dispose()
        {
            RebuildDatabaseTables();
        }
    }
}
