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
            ActivityList = GetAllActivities();
            SelectedActivity = ActivityList.FirstOrDefault();
            UserActivityEntity = new UserActivityModel();

            HoursSelector = Enumerable.Range(0, 24).ToList();

            MinutesSelector = new List<int>() { 0, 30 };
            MinutesSelector.AddRange(Enumerable.Range(0, 60).ToList());
            MinutesSelector = MinutesSelector.Distinct().ToList();

            LoadUserActivityListCommand = new RelayCommand(LoadUserActivityListAsync);
            AddUserActivityItemCommand = new RelayCommand(AddUserActivityItem);
        }

        private void LoadUserActivityList()
        {
            UserActivityList = GetAllUserActivities();
        }

        private async void LoadUserActivityListAsync()
        {
            UserActivityList = await GetAllUserActivitiesAsync();
        }

        private ObservableCollection<UserActivityModel> GetAllUserActivities()
        {
            var userActivityList = new ObservableCollection<UserActivityModel>();

            UserActivityService.GetExtendedAll().ToList().ForEach((item) =>
            {
                userActivityList.Add((UserActivityModel)item);
            });

            return userActivityList;
        }

        private async Task<ObservableCollection<UserActivityModel>> GetAllUserActivitiesAsync()
        {
            var userActivityList = new ObservableCollection<UserActivityModel>();
            var bllList = await UserActivityService.GetExtendedAllAsync();

            bllList.ToList().ForEach((item) =>
            {
                userActivityList.Add((UserActivityModel)item);
            });

            return userActivityList;
        }

        private void AddUserActivityItem()
        {
            UserActivityService.Add(new UserActivitiesFullDTO() {
                UserId     = ViewModelBase.CurrentUserProvider.Id,
                ActivityId = SelectedActivity.Id,
                Duration   = UserActivityEntity.DurationHours * 3600 + UserActivityEntity.DurationMinutes * 60,
                Comment    = UserActivityEntity.Comment,
                Date       = DateTime.Now,
            });

            LoadUserActivityList();

            //init form
            UserActivityEntity = new UserActivityModel();
        }
    }
}

