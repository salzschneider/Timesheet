using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Managers;
using Timesheet.DAL.Timesheet;
using Timesheet.UI.Commands;
using Timesheet.UI.Models;
using Timesheet.UI.Utilities;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace Timesheet.UI.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        private UserModel currentUser;

        private ViewModelBase currentViewModel;

        public ICommand OpenActivityMgmtCommand { get; }
 
        public ICommand OpenUserMgmtCommand { get; }

        public ICommand OpenWorklogMgmtCommand { get; }

        public ICommand OpenReportMgmtCommand { get; }


        public ViewModelBase SelectedViewModel
        {
            get { return currentViewModel; }
            set { OnPropertyChanged<ViewModelBase>(ref currentViewModel, value); }
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
        /// Title of the main application window 
        /// </summary>
        public string Title { get; set; }

        public ViewModelBase()
        {
            SelectedViewModel = this;
            Title = ApplicationConfig.ApplicationName + " " + ApplicationConfig.ApplicationVersion;

            OpenActivityMgmtCommand = new RelayCommand(OpenActivityMgmt);
            OpenUserMgmtCommand = new RelayCommand(OpenUserMgmt);
            OpenWorklogMgmtCommand = new RelayCommand(OpenWorklogMgmt);
            OpenReportMgmtCommand = new RelayCommand(OpenReportMgmt);

            if (ViewModelBase.CurrentUserProvider == null)
            {
                CurrentUser = new UserModel();

                //1. user is the admin now
                UsersToCurrentUser(UserManager.GetUser(1));

                ViewModelBase.CurrentUserProvider = CurrentUser;
            }
            else
            {
                CurrentUser = ViewModelBase.CurrentUserProvider;
            }
        }

        private void OpenActivityMgmt()
        {
            SelectedViewModel = new ActivityMgmtViewModel();
        }

        private void OpenUserMgmt()
        {
            SelectedViewModel = new UserMgmtViewModel();
        }

        private void OpenWorklogMgmt()
        {
            SelectedViewModel = new WorklogViewModel();
        }

        private void OpenReportMgmt()
        {
            SelectedViewModel = new ReportViewModel();
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

        protected void UsersToCurrentUser(Users users)
        {
            CurrentUser.Id       = users.Id;
            CurrentUser.Username = users.Username;
            CurrentUser.Password = users.Password;
            CurrentUser.FullName = users.FullName;
        }
    }
}
