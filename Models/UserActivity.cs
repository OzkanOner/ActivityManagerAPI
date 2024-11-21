namespace ActivityManagerAPI.Models
{
    public class UserActivity : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int AssignerUserId { get; set; }
        public User AssignerUser { get; set; }

        public bool IsCompleted { get; set; }
    }
}
