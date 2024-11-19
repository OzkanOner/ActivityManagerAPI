namespace ActivityManagerAPI.Models.Dtos
{
    public class ActivityDTO : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public int CreatedUserId { get; set; }

        public UserDTO CreatedUser { get; set; }
    }
}
