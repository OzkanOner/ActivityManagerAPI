using ActivityManagerAPI.Models;

namespace ActivityManagerAPI.Repositories.Abstract
{
    public interface IUserActivityRepository : IGenericRepository<UserActivity>
    {
        Task<UserActivity?> GetUserActivityByUserAndActivity(int userId, int activityId);
    }
}
