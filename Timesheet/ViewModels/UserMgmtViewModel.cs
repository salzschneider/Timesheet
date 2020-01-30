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
    public class UserMgmtViewModel : ViewModelBase
    {
        private ObservableCollection<UserModel> userList;

        private UserModel userModelEntity;

        private UserModel selectedUser;

        public ObservableCollection<UserModel> UserList
        {
            get { return this.userList; }
            set { OnPropertyChanged<ObservableCollection<UserModel>>(ref this.userList, value); }
        }

        public UserModel UserModelEntity
        {
            get { return this.userModelEntity; }
            set { OnPropertyChanged<UserModel>(ref this.userModelEntity, value); }
        }

        /// <summary>
        /// Current user from the UserList
        /// </summary>
        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set { OnPropertyChanged<UserModel>(ref selectedUser, value); }
        }

        public ICommand LoadUserListCommand { get; }

        public ICommand AddUserItemCommand { get; }

        public ICommand UserChangeCommand { get; }

        public UserMgmtViewModel()
        {
            LoadUserList();

            UserModelEntity = new UserModel();
            LoadUserListCommand = new RelayCommand(LoadUserList);
            AddUserItemCommand = new RelayCommand(AddUserItem);
            UserChangeCommand = new RelayCommand(UserChange);
        }

        /// <summary>
        /// It can be triggered by LoadUserList. In this case SelectedUser will be null. 
        /// </summary>
        private void UserChange()
        {
            if(SelectedUser != null)
            {
                //Deep copy not to lose CurrentUser data
                CurrentUser = Utility.CloneObject(SelectedUser) as UserModel;
                ViewModelBase.CurrentUserProvider = CurrentUser;
            }
            else
            {
                CurrentUser = ViewModelBase.CurrentUserProvider;
                SelectedUser = UserList.First(x => x.Id == ViewModelBase.CurrentUserProvider.Id);
            }
        }

        private void LoadUserList()
        {
            UserList = GetAllUsers();

            if(ViewModelBase.CurrentUserProvider != null)
            {
                SelectedUser = UserList.First(x => x.Id == ViewModelBase.CurrentUserProvider.Id);
            }
        }

        private void AddUserItem()
        {
            UserManager.AddUserItem(UserModelEntity.Username, UserModelEntity.Password, UserModelEntity.FullName);

            LoadUserList();
            UserModelEntity = new UserModel();
        }

        public static ObservableCollection<UserModel> GetAllUsers()
        {
            var userList = UserManager.GetAllUsers()
                .Select(user => new UserModel
                {
                    Id       = user.Id,
                    Username = user.Username,
                    FullName = user.FullName,
                    Password = user.Password,

                }).ToList();

            return new ObservableCollection<UserModel>(userList); 
        }
    }
}

