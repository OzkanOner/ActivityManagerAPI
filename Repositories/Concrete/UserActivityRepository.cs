using ActivityManagerAPI.Data;
using ActivityManagerAPI.Models;
using ActivityManagerAPI.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ActivityManagerAPI.Repositories.Concrete
{
    public class UserActivityRepository : GenericRepository<UserActivity>, IUserActivityRepository
    {
        private readonly AppDbContext _context;

        public UserActivityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserActivity?> GetUserActivityByUserAndActivity(int userId, int activityId)
        {
            return await _context.Set<UserActivity>().FirstOrDefaultAsync(ua => ua.UserId == userId && ua.ActivityId == activityId);
        }
    }
}
