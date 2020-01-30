namespace Timesheet.DAL.Timesheet
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Expanding the original UserActivities fields
    /// </summary>
    public class UserActivitiesDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public int ActivityId { get; set; }

        public string ActivityName { get; set; }

        public int Duration { get; set; }

        public string Comment { get; set; }

        public int SumDuration { get; set; }

        public System.DateTime Date { get; set; }
    }
}
