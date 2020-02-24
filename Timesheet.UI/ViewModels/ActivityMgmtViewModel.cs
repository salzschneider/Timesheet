using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Timesheet.Core.DTO;
using Timesheet.BLL.Factories;
using Timesheet.BLL.Services;
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
            ReloadActivityListCommand = new RelayCommand(async() => ActivityList = await GetAllActivitiesAsync());
            AddActivityItemCommand = new RelayCommand(AddActivityItem);
        }

        private void AddActivityItem()
        {
            ActivityService.Add(new ActivitiesFullDTO() {
                    Title = ActivityModelEntity.Title,
                    Description = ActivityModelEntity.Description,
            });

            ReloadActivityListCommand.Execute(null);
            ActivityModelEntity = new ActivityModel();
        }
    }
}
