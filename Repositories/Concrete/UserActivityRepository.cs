using ActivityManagerAPI.Data;
using ActivityManagerAPI.Models;
using ActivityManagerAPI.Models.Dtos;
using ActivityManagerAPI.Repositories.Abstract;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ActivityManagerAPI.Repositories.Concrete
{
    public class UserActivityRepository : GenericRepository<UserActivity>, IUserActivityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserActivityRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserActivity?> GetUserActivityByUserAndActivity(int userId, int activityId)
        {
            return await _context.Set<UserActivity>()
                .Include(ua => ua.User)
                .Include(ua => ua.Activity)
                .Include(ua => ua.AssignerUser)
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.ActivityId == activityId);
        }

        public async Task<UserActivityDTO?> GetUserActivityByUserAndActivityDTO(int userId, int activityId)
        {
            var userActivity = await _context.UserActivities
                .Include(ua => ua.User)
                .Include(ua => ua.Activity)
                .Include(ua => ua.AssignerUser)
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.ActivityId == activityId);

            if (userActivity == null)
                return null;

            var userActivityDTO = _mapper.Map<UserActivityDTO>(userActivity);

            return userActivityDTO;
        }
    }
}
