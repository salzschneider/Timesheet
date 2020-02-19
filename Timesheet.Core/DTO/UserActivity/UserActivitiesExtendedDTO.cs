namespace Timesheet.Core.DTO
{
    /// <summary>
    /// Data Transfer Object for a UserActivities Entity with additional (Username, Activity Name) fields
    /// </summary>
    public class UserActivitiesExtendedDTO : UserActivitiesFullDTO
    {
        public string Username { get; set; }

        public string ActivityName { get; set; }
    }
}
