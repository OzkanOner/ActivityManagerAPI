namespace ActivityManagerAPI.Models.Dtos
{
    public class UserActivityDTO : BaseEntity
    {
        public int UserId { get; set; }
        public UserDTO User { get; set; }

        public int ActivityId { get; set; }
        public ActivityDTO Activity { get; set; }

        public int AssignerUserId { get; set; }
        public UserDTO AssignerUser { get; set; }

        public bool IsCompleted { get; set; }
    }
}
