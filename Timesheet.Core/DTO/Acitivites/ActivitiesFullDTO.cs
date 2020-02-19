namespace Timesheet.Core.DTO
{
    /// <summary>
    /// Data Transfer Object for an Activities Entity
    /// </summary>
    public class ActivitiesFullDTO : BaseDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
