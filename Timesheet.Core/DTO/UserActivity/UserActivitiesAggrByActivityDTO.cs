namespace Timesheet.Core.DTO
{
    /// <summary>
    /// Data Transfer Object for UserActivities Aggregated by Activity
    /// </summary>
    public class UserActivitiesAggrByActivityDTO
    {
        public int ActivityId { get; set; }

        public string ActivityName { get; set; }

        public int SumDuration { get; set; }
    }
}
