using ActivityManagerAPI.Models;
using ActivityManagerAPI.Models.Dtos;

namespace ActivityManagerAPI.Repositories.Abstract
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        Task<List<Activity>> GetAllActivitiesWithRelations();
        Task<Activity?> GetActivityByIdWithRelations(int id);
        Task<Activity?> UpdateActivity(ActivityUpdateDto activityUpdateDto);
    }
}