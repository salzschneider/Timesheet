namespace Timesheet.Core.DTO
{
    /// <summary>
    /// Data Transfer Object for a Users Entity
    /// </summary>
    public class UsersFullDTO : BaseDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }
    }
}
