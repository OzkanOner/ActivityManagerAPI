using ActivityManagerAPI.Data;
using ActivityManagerAPI.Models;
using ActivityManagerAPI.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ActivityManagerAPI.Repositories.Concrete
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        private readonly AppDbContext _context;

        public ActivityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Activity?> GetActivityByIdWithRelations(int id)
        {
            return await _context.Activities
                .Include(a => a.CreatedUser)
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
    }
}