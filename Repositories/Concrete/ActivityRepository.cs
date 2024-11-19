using ActivityManagerAPI.Models;
using ActivityManagerAPI.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ActivityManagerAPI.Repositories.Concrete
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(DbContext context) : base(context)
        {
        }
    }
}