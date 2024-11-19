using ActivityManagerAPI.Models;
using ActivityManagerAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityRepository _activityRepository;

        public ActivitiesController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        [HttpGet]
        public Task<IActionResult> GetAllActivities()
        {
            List<Activity> activities = _activityRepository.GetAll().ToList();
            if (activities == null || activities.Count == 0)
                return Task.FromResult<IActionResult>(NoContent());

            return Task.FromResult<IActionResult>(Ok(activities));
        }

        [HttpPost]
        public Task<IActionResult> AddActivity(Activity activity)
        {
            var result = _activityRepository.Create(activity);
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}