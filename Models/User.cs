namespace ActivityManagerAPI.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<UserActivity> UserActivities { get; set; } = new List<UserActivity>();
        public ICollection<UserActivity> AssignedActivities { get; set; } = new List<UserActivity>();
    }
}