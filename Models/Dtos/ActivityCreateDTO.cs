namespace ActivityManagerAPI.Models.Dtos
{
    public class ActivityCreateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public int CreatedUserId { get; set; }
    }
}