using ActivityManagerAPI.Models;
using ActivityManagerAPI.Models.Dtos;
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
        public async Task<IActionResult> GetAllActivities()
        {
            var result = await _activityRepository.GetAll();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(int id)
        {
            var result = await _activityRepository.GetById(id);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityCreateDTO activityCreateDTO)
        {
            if (activityCreateDTO == null)
            {
                return BadRequest("Missing activity data!");
            }

            var activity = new Activity
            {
                Title = activityCreateDTO.Title,
                Description = activityCreateDTO.Description,
                DueDate = activityCreateDTO.DueDate,
                CreatedUserId = activityCreateDTO.CreatedUserId
            };

            var result = await _activityRepository.Create(activity);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateActivity([FromBody] Activity activity)
        {
            if (activity == null)
            {
                return BadRequest("Missing activity data!");
            }

            var result = await _activityRepository.Update(activity);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var result = await _activityRepository.Delete(id);
            return result;
        }
    }
}