using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Timesheet.Core.DTO;
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

            //loading all activities
            LoadSumDurationByActivitiesCommand = new RelayCommand(() => UserSumDurationByActivityList = GetSumDurationByActivities(FilterStartDate, FilterEndDate));
            SumActivityExportToCSVCommand = new RelayCommand(SumActivityExportToCSV);

            //user activities
            UserActivitiesDurationList = new ObservableCollection<UserActivityModel>();
            LoadUserActivitiesDurationCommand = new RelayCommand(LoadUserActivitiesDurationList);
            UserActivityDurationExportToCSVCommand = new RelayCommand(UserActivityDurationExportToCSV);

            //user list combo
            UserList = GetAllUsers();
            SelectedUser = UserList.FirstOrDefault();
        }

        private void SumActivityExportToCSV()
        {
            var headerList = new List<string>(){"Activity", "SumDuration"};

            string[][] csvArray = new string[UserSumDurationByActivityList.Count()][];

            for (int i = 0; i < UserSumDurationByActivityList.Count(); i++)
            {
                csvArray[i] = new string[] { UserSumDurationByActivityList[i].ActivityName,
                                             UserSumDurationByActivityList[i].SumDurationReadable.ToString() };
            }

            SaveFileAs(Helper.ArrayToCSV(csvArray, headerList));
        }

        private void UserActivityDurationExportToCSV()
        {
            var headerList = new List<string>() { "Username", "Activity", "SumDuration" };

            string[][] csvArray = new string[UserActivitiesDurationList.Count()][];

            for (int i = 0; i < UserActivitiesDurationList.Count(); i++)
            {
                csvArray[i] = new string[] { selectedUser.Username,
                                             UserActivitiesDurationList[i].ActivityName,
                                             UserActivitiesDurationList[i].SumDurationReadable.ToString() };
            }

            SaveFileAs(Helper.ArrayToCSV(csvArray, headerList));
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
            var userActivityList = UserActivityService.GetSumDurationByActivities(startDate, endDate)
                .Select(userActivity => new UserActivityModel
                {
                    ActivityId          = userActivity.ActivityId,
                    ActivityName        = userActivity.ActivityName,
                    SumDuration         = userActivity.SumDuration,
                    SumDurationReadable = (userActivity.SumDuration < TimeSpan.MaxValue.TotalSeconds) ? TimeSpan.FromSeconds(userActivity.SumDuration).ToString(@"hh\:mm\:ss") : "Too much",
                }).ToList();

            return new ObservableCollection<UserActivityModel>(userActivityList);
        }

        private ObservableCollection<UserActivityModel> GetUserActivitiesDuration(int userId, DateTime startDate, DateTime endDate)
        {
            var userActivityList = UserActivityService.GetSumDurationByActivities(startDate, endDate, userId)
                .Select(userActivity => new UserActivityModel
                {
                    ActivityId          = userActivity.ActivityId,
                    ActivityName        = userActivity.ActivityName,
                    UserId              = SelectedUser.Id,
                    Username            = SelectedUser.Username,
                    SumDuration         = userActivity.SumDuration,
                    SumDurationReadable = (userActivity.SumDuration < TimeSpan.MaxValue.TotalSeconds) ? TimeSpan.FromSeconds(userActivity.SumDuration).ToString(@"hh\:mm\:ss") : "Too much",
                }).ToList();

            return new ObservableCollection<UserActivityModel>(userActivityList);
        }
    }
}

