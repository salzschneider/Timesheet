using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.UI.Utilities;

namespace Timesheet.UI.Models
{
    public class ActivityModel : ObservableObject 
    {
        private string title = "";
        private string description = "";

        public int Id { get; set; }

        public string Title
        {
            get { return title; }
            set { OnPropertyChanged<string>(ref title, value); }
        }

        public string Description
        {
            get { return description; }
            set { OnPropertyChanged<string>(ref description, value); }
        }
    }
}
