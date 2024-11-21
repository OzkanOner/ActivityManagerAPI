namespace ActivityManagerAPI.Models.Dtos
{
    public class ActivityAssignedUserDTO : BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public bool IsCompleted { get; set; }
    }
}
