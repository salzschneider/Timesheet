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
    public class WorklogViewModel : ViewModelBase
    {
        private ObservableCollection<UserActivityModel> userActivityList;

        private ActivityModel selectedActivity;

        private ObservableCollection<ActivityModel> activityList;

        private UserActivityModel userActivityEntity;

        public ObservableCollection<UserActivityModel> UserActivityList
        {
            get { return this.userActivityList; }
            set { OnPropertyChanged<ObservableCollection<UserActivityModel>>(ref this.userActivityList, value); }
        }

        public ActivityModel SelectedActivity
        {
            get { return selectedActivity; }
            set { OnPropertyChanged<ActivityModel>(ref selectedActivity, value); }
        }

        public ObservableCollection<ActivityModel> ActivityList
        {
            get { return this.activityList; }
            set { OnPropertyChanged<ObservableCollection<ActivityModel>>(ref this.activityList, value); }
        }

        public UserActivityModel UserActivityEntity
        {
            get { return this.userActivityEntity; }
            set { OnPropertyChanged<UserActivityModel>(ref this.userActivityEntity, value); }
        }

        public List<int> HoursSelector { get; set; }

        public List<int> MinutesSelector { get; set; }

        public ICommand LoadUserActivityListCommand { get; }

        public ICommand AddUserActivityItemCommand { get; }

        public WorklogViewModel()
        {
            LoadUserActivityList();
            ActivityList = ActivityMgmtViewModel.GetAllActivities();
            SelectedActivity = ActivityList.FirstOrDefault();
            UserActivityEntity = new UserActivityModel();

            HoursSelector = Enumerable.Range(0, 25).ToList();

            MinutesSelector = new List<int>() { 0, 30 };
            MinutesSelector.AddRange(Enumerable.Range(0, 61).ToList());
            MinutesSelector = MinutesSelector.Distinct().ToList();

            LoadUserActivityListCommand = new RelayCommand(LoadUserActivityList);
            AddUserActivityItemCommand = new RelayCommand(AddUserActivityItem);
        }

        private void LoadUserActivityList()
        {
            UserActivityList = GetAllUserActivities();
        }

        private ObservableCollection<UserActivityModel> GetAllUserActivities()
        {
            var userActivityList = UserActivityManager.GetAllUserActivities()
                .Select(userActivity => new UserActivityModel
                {
                    Id = userActivity.Id,
                    UserId = userActivity.UserId,
                    Username = userActivity.Username,
                    ActivityId = userActivity.ActivityId,
                    ActivityName = userActivity.ActivityName,
                    Duration = userActivity.Duration,
                    DurationReadable = (userActivity.Duration < TimeSpan.MaxValue.TotalSeconds) ? TimeSpan.FromSeconds(userActivity.Duration).ToString(@"hh\:mm\:ss") : "Too much",
                    Comment = userActivity.Comment,
                    Date = userActivity.Date,

                }).ToList();

            return new ObservableCollection<UserActivityModel>(userActivityList);
        }

        private void AddUserActivityItem()
        {
            UserActivityManager.AddUserActivityItem(ViewModelBase.CurrentUserProvider.Id, 
                                                    SelectedActivity.Id,
                                                    UserActivityEntity.DurationHours * 3600 + UserActivityEntity.DurationMinutes * 60,
                                                    UserActivityEntity.Comment,
                                                    DateTime.Now);

            LoadUserActivityList();

            //init form
            UserActivityEntity = new UserActivityModel();
        }
    }
}

