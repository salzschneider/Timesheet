namespace Timesheet.Core.DTO
{
    /// <summary>
    /// Data Transfer Object for an UserActivities Entity 
    /// </summary>
    public class UserActivitiesFullDTO : BaseDTO
    {
        public int UserId { get; set; }

        public int ActivityId { get; set; }

        public int Duration { get; set; }

        public string Comment { get; set; }

        public System.DateTime Date { get; set; }
    }
}
