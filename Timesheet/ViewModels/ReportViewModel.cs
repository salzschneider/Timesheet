using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Timesheet.DAL.Managers;
using Timesheet.DAL.Timesheet;
using Timesheet.UI.Commands;
using Timesheet.UI.Models;
using Timesheet.UI.Utilities;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;


namespace Timesheet.UI.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private UserModel selectedUser;

        private ObservableCollection<UserModel> userList;

        private ObservableCollection<UserActivityModel> userSumDurationByActivityList;

        private ObservableCollection<UserActivityModel> userActivitiesDurationList;

        #region User tab public fields and properties

        /// <summary>
        /// Selected user from the user combo box
        /// </summary>
        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set { OnPropertyChanged<UserModel>(ref selectedUser, value); }
        }

        public ObservableCollection<UserModel> UserList
        {
            get { return this.userList; }
            set { OnPropertyChanged<ObservableCollection<UserModel>>(ref this.userList, value); }
        }

        public ObservableCollection<UserActivityModel> UserActivitiesDurationList
        {
            get { return this.userActivitiesDurationList; }
            set { OnPropertyChanged<ObservableCollection<UserActivityModel>>(ref this.userActivitiesDurationList, value); }
        }
        #endregion

        public ObservableCollection<UserActivityModel> UserSumDurationByActivityList
        {
            get { return this.userSumDurationByActivityList; }
            set { OnPropertyChanged<ObservableCollection<UserActivityModel>>(ref this.userSumDurationByActivityList, value); }
        }

        public DateTime FilterStartDate { get; set; } = DateTime.Now.AddDays(-31);

        public DateTime FilterEndDate { get; set; } = DateTime.Now;

        #region Activity tab commands
        public ICommand LoadSumDurationByActivitiesCommand { get; }

        public ICommand SumActivityExportToCSVCommand { get; }
        #endregion 

        #region User tab commands
        public ICommand LoadUserActivitiesDurationCommand { get; }

        public ICommand UserActivityDurationExportToCSVCommand { get; }
        #endregion 


        public ReportViewModel()
        {
            //activities
            UserSumDurationByActivityList = new ObservableCollection<UserActivityModel>();
            LoadSumDurationByActivitiesCommand = new RelayCommand(LoadSumDurationByActivitiesList);
            SumActivityExportToCSVCommand = new RelayCommand(SumActivityExportToCSV);

            //user activities
            UserActivitiesDurationList = new ObservableCollection<UserActivityModel>();
            LoadUserActivitiesDurationCommand = new RelayCommand(LoadUserActivitiesDurationList);
            UserActivityDurationExportToCSVCommand = new RelayCommand(UserActivityDurationExportToCSV);

            //user list combo
            UserList = UserMgmtViewModel.GetAllUsers();
            SelectedUser = UserList.FirstOrDefault();
        }

        private void SumActivityExportToCSV()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("\"Activity\",\"SumDuration\"");

            string line;

            foreach (var item in UserSumDurationByActivityList)
            {
                line = "\"" + item.ActivityName.Replace("\"", "\"\"") + "\"" + "," + "\"" + item.SumDurationReadable.Replace("\"", "\"\"") + "\"";
                sb.AppendLine(line);
            }

            SaveFileAs(sb.ToString());
        }

        private void UserActivityDurationExportToCSV()
        {
            if (SelectedUser != null)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("\"Username\",\"Activity\",\"SumDuration\"");

                string line;

                foreach (var item in UserActivitiesDurationList)
                {
                    line = "\"" + selectedUser.Username.Replace("\"", "\"\"") + "\"" + "," 
                           + "\"" + item.ActivityName.Replace("\"", "\"\"") + "\"" + "," 
                           + "\"" + item.SumDurationReadable.Replace("\"", "\"\"") + "\"";
                    sb.AppendLine(line);
                }

                SaveFileAs(sb.ToString());
            }
        }

        //all activity
        private void LoadSumDurationByActivitiesList()
        {
            UserSumDurationByActivityList = GetSumDurationByActivities(FilterStartDate, FilterEndDate);
        }

        //Filtered by user
        private void LoadUserActivitiesDurationList()
        {
            if(SelectedUser != null)
            {
                UserActivitiesDurationList = GetUserActivitiesDuration(SelectedUser.Id, FilterStartDate, FilterEndDate);
            }
        }

        private ObservableCollection<UserActivityModel> GetSumDurationByActivities(DateTime startDate, DateTime endDate)
        {
            var userActivityList = UserActivityManager.GetSumDurationByActivities(startDate, endDate)
                .Select(userActivity => new UserActivityModel
                {
                    ActivityId = userActivity.ActivityId,
                    ActivityName = userActivity.ActivityName,
                    SumDuration = userActivity.SumDuration,
                    SumDurationReadable = (userActivity.SumDuration < TimeSpan.MaxValue.TotalSeconds) ? TimeSpan.FromSeconds(userActivity.SumDuration).ToString(@"hh\:mm\:ss") : "Too much",
                }).ToList();

            return new ObservableCollection<UserActivityModel>(userActivityList);
        }

        private ObservableCollection<UserActivityModel> GetUserActivitiesDuration(int userId, DateTime startDate, DateTime endDate)
        {
            var userActivityList = UserActivityManager.GetUserActivitiesDuration(userId, startDate, endDate)
                .Select(userActivity => new UserActivityModel
                {
                    ActivityId = userActivity.ActivityId,
                    ActivityName = userActivity.ActivityName,
                    UserId = SelectedUser.Id,
                    Username = SelectedUser.Username,
                    SumDuration = userActivity.SumDuration,
                    SumDurationReadable = (userActivity.SumDuration < TimeSpan.MaxValue.TotalSeconds) ? TimeSpan.FromSeconds(userActivity.SumDuration).ToString(@"hh\:mm\:ss") : "Too much",
                }).ToList();

            return new ObservableCollection<UserActivityModel>(userActivityList);
        }
    }
}

