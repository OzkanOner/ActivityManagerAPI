using ActivityManagerAPI.Models;

namespace ActivityManagerAPI.Repositories.Abstract
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        Task<List<Activity>> GetAllActivitiesWithRelations();
        Task<Activity?> GetActivityByIdWithRelations(int id);
    }
}