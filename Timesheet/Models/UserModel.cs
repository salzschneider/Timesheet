﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.UI.Utilities;

namespace Timesheet.UI.Models
{
    public class UserModel : ObservableObject 
    {
        private string username = "";
        private string fullName = "";
        private string password = "";

        public int Id { get; set; }

        public string Username
        {
            get { return username; }
            set { OnPropertyChanged<string>(ref username, value); }
        }

        public string FullName
        {
            get { return fullName; }
            set { OnPropertyChanged<string>(ref fullName, value); }
        }

        public string Password
        {
            get { return password; }
            set { OnPropertyChanged<string>(ref password, value); }
        }
    }
}
