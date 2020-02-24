using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Core.DTO;
using Timesheet.UI.Utilities;

namespace Timesheet.UI.Models
{
    public class UserActivityModel : ObservableObject 
    {
        private string comment = "";
        private int duration = 0;
        private int durationHours = 0;
        private int durationMinutes = 0;

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public int ActivityId { get; set; }

        public string ActivityName { get; set; }

        public int Duration
        {
            get { return duration; }
            set { OnPropertyChanged<int>(ref duration, value); }
        }

        public int DurationHours
        {
            get { return durationHours; }
            set { OnPropertyChanged<int>(ref durationHours, value); }
        }

        public int DurationMinutes
        {
            get { return durationMinutes; }
            set { OnPropertyChanged<int>(ref durationMinutes, value); }
        }

        public string DurationReadable { get; set; }

        public int SumDuration { get; set; }

        public string SumDurationReadable { get; set; }

        public int UserActivityDuration { get; set; }

        public int UserActivityDurationReadable { get; set; }

        public string Comment
        {
            get { return comment; }
            set { OnPropertyChanged<string>(ref comment, value); }
        }

        public System.DateTime? Date { get; set; }


        public static explicit operator UserActivityModel(UserActivitiesExtendedDTO userActivity)
        {
            UserActivityModel userActivityModel = new UserActivityModel()
            {
                Id               = userActivity.Id,
                UserId           = userActivity.UserId,
                Username         = userActivity.Username,
                ActivityId       = userActivity.ActivityId,
                ActivityName     = userActivity.ActivityName,
                Duration         = userActivity.Duration,
                DurationReadable = Helper.SecondsToHourMinSec(userActivity.Duration),
                Comment          = userActivity.Comment,
                Date             = userActivity.Date,
            };

            return userActivityModel;
        }

        public static explicit operator UserActivityModel(UserActivitiesAggrByActivityDTO userAggrActivity)
        {
            UserActivityModel userActivityModel = new UserActivityModel()
            {
                ActivityId   = userAggrActivity.ActivityId,
                ActivityName = userAggrActivity.ActivityName,
                SumDuration  = userAggrActivity.SumDuration,
            };

            return userActivityModel;
        }
    }
}
