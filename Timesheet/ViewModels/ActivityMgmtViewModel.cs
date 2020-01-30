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
    public class ActivityMgmtViewModel : ViewModelBase
    {
        private ObservableCollection<ActivityModel> activityList;

        private ActivityModel activityModelEntity;

        public ObservableCollection<ActivityModel> ActivityList
        {
            get { return this.activityList; }
            set { OnPropertyChanged<ObservableCollection<ActivityModel>>(ref this.activityList, value);}
        }

        public ActivityModel ActivityModelEntity
        {
            get { return this.activityModelEntity; }
            set { OnPropertyChanged<ActivityModel>(ref this.activityModelEntity, value); }
        }

        public ICommand ReloadActivityListCommand { get; }

        public ICommand AddActivityItemCommand { get; }

        public ActivityMgmtViewModel()
        {
            ActivityList = GetAllActivities();
            ActivityModelEntity = new ActivityModel();
            ReloadActivityListCommand = new RelayCommand(ReloadActivityList);
            AddActivityItemCommand = new RelayCommand(AddActivityItem);
        }

        private void ReloadActivityList()
        {
            ActivityList = GetAllActivities();
        }

        private void AddActivityItem()
        {
            ActivityManager.AddActivityItem(ActivityModelEntity.Title, ActivityModelEntity.Description);

            ReloadActivityListCommand.Execute(null);
            ActivityModelEntity = new ActivityModel();
        }

        public static ObservableCollection<ActivityModel> GetAllActivities()
        {
            var activityList = ActivityManager.GetAllActivities()
                .Select(activity => new ActivityModel
                {
                    Id          = activity.Id,
                    Title       = activity.Title,
                    Description = activity.Description,

                }).ToList();

            return new ObservableCollection<ActivityModel>(activityList);
        }
    }
}
