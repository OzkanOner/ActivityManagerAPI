using ActivityManagerAPI.Models.Abstract;

namespace ActivityManagerAPI.Models
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
