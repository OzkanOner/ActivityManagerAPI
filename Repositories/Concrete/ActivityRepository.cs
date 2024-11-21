using ActivityManagerAPI.Data;
using ActivityManagerAPI.Models;
using ActivityManagerAPI.Models.Dtos;
using ActivityManagerAPI.Repositories.Abstract;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ActivityManagerAPI.Repositories.Concrete
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ActivityRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Activity?> GetActivityByIdWithRelations(int id)
        {
            return await _context.Activities
                .Include(a => a.CreatedUser)
                .Include(a => a.UserActivities)
                    .ThenInclude(ua => ua.User)
                .Include(a => a.UserActivities)
                    .ThenInclude(ua => ua.AssignerUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Activity>> GetAllActivitiesWithRelations()
        {
            return await _context.Activities
                .Include(a => a.CreatedUser)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Activity> UpdateActivity(ActivityUpdateDto activityUpdateDto)
        {
            try
            {
                var existingEntity = await _context.Activities.FindAsync(activityUpdateDto.Id);

                if (existingEntity == null)
                {
                    return null;
                }

                existingEntity.Title = activityUpdateDto.Title;
                existingEntity.Description = activityUpdateDto.Description;
                existingEntity.DueDate = activityUpdateDto.DueDate;
                existingEntity.IsCompleted = activityUpdateDto.IsCompleted;

                await _context.SaveChangesAsync();

                return existingEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}