using ActivityManagerAPI.Models.Abstract;

namespace ActivityManagerAPI.Models
{
    public class Activity : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public int CreatedUserId { get; set; }

        public User CreatedUser { get; set; }
        public ICollection<UserActivity> UserActivities { get; set; } = new List<UserActivity>();
    }
}