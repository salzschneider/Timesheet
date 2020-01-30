using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Timesheet;

namespace Timesheet.DAL.Managers
{
    public class UserManager
    {
        public static void AddUserItem(string username, string password, string fullName)
        {
            using (var db = new TimesheetEntities())
            {
                var userItem = new Users()
                {
                    Username = username,
                    Password = password,
                    FullName = fullName,
                };

                db.Users.Add(userItem);
                db.SaveChanges();
            }
        }

        public static List<Users> GetAllUsers()
        {
            var allUserList = new List<Users>();

            using (var db = new TimesheetEntities())
            {
                var queryResult = db.Users.OrderBy(c => c.Id);

                allUserList = queryResult.ToList();
            }

            return allUserList;
        }

        public static Users GetUser(int id)
        { 
            using (var db = new TimesheetEntities())
            {
                return db.Users.First( u => u.Id == id);
            }
        }
    }
}
