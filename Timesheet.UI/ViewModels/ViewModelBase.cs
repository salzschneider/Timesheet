using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Core.DTO;
using Timesheet.BLL.Factories;
using Timesheet.BLL.Services;
using Timesheet.UI.Commands;
using Timesheet.UI.Models;
using Timesheet.UI.Utilities;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;

namespace Timesheet.UI.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        private UserModel currentUser;

        private ViewModelBase currentViewModel;

        private bool isSignedIn = ViewModelBase.CurrentUserProvider != null;

        protected ActivityService ActivityService { get; set; }

        protected UserService UserService { get; set; }

        protected UserActivityService UserActivityService { get; set; }

        public ICommand OpenActivityMgmtCommand { get; }
 
        public ICommand OpenUserMgmtCommand { get; }

        public ICommand OpenWorklogMgmtCommand { get; }

        public ICommand OpenReportMgmtCommand { get; }

        public ICommand SignInCommand { get; }

        public ViewModelBase SelectedViewModel
        {
            get { return currentViewModel; }
            set { OnPropertyChanged<ViewModelBase>(ref currentViewModel, value); }
        }

        public bool IsSignedIn
        {
            get { return isSignedIn; }
            set { OnPropertyChanged<bool>(ref isSignedIn, value); }
        }

        /// <summary>
        /// Application wide value for other ModelViews
        /// </summary>
        public static UserModel CurrentUserProvider;

        public UserModel CurrentUser
        {
            get { return currentUser; }
            set { OnPropertyChanged<UserModel>(ref currentUser, value); }
        }

        /// <summary>
        /// Title on the main application window 
        /// </summary>
        public string Title { get; set; }

        public ViewModelBase()
        {
            SelectedViewModel = this;
            Title = ApplicationConfig.ApplicationName + " " + ApplicationConfig.ApplicationVersion;

            ActivityService = (ActivityService)ServiceFactory.CreateActivityService();
            UserService = (UserService)ServiceFactory.CreateUserService();
            UserActivityService = (UserActivityService)ServiceFactory.CreateUserActivityService();

            OpenActivityMgmtCommand = new RelayCommand(() => SelectedViewModel = new ActivityMgmtViewModel());
            OpenUserMgmtCommand = new RelayCommand(() => SelectedViewModel = new UserMgmtViewModel());
            OpenWorklogMgmtCommand = new RelayCommand(() => SelectedViewModel = new WorklogViewModel());
            OpenReportMgmtCommand = new RelayCommand(() => SelectedViewModel = new ReportViewModel());
            SignInCommand = new RelayCommand(SignIn);

            if (IsSignedIn)
            {
                CurrentUser = ViewModelBase.CurrentUserProvider;
            }
        }

        private void SignIn()
        {
            if (!IsSignedIn)
            {
                //1. user is the admin now
                CurrentUser = Helper.CreateUserModel(UserService.GetById(1));
                ViewModelBase.CurrentUserProvider = CurrentUser;

                IsSignedIn = true;
            }
        }

        protected void SaveFileAs(string csvContent)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, csvContent);
            }
        }

        public ObservableCollection<ActivityModel> GetAllActivities()
        {
            var activityList = new ObservableCollection<ActivityModel>();

            ActivityService.GetAll().ForEach((item) =>
            {
                activityList.Add((ActivityModel)item);
            });

            return activityList;
        }

        public ObservableCollection<UserModel> GetAllUsers()
        {
            var userList = new ObservableCollection<UserModel>();

            UserService.GetAll().ForEach((item) =>
            {
                userList.Add((UserModel)item);
            });

            return userList;
        }
    }
}
