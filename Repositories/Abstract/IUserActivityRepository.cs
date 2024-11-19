using ActivityManagerAPI.Models;
using ActivityManagerAPI.Models.Dtos;

namespace ActivityManagerAPI.Repositories.Abstract
{
    public interface IUserActivityRepository : IGenericRepository<UserActivity>
    {
        Task<UserActivityDTO?> GetUserActivityByUserAndActivityDTO(int userId, int activityId);
        Task<UserActivity?> GetUserActivityByUserAndActivity(int userId, int activityId);
    }
}
