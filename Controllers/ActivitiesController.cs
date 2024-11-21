using ActivityManagerAPI.Models.Dtos;
using ActivityManagerAPI.Repositories.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllActivities()
        {
            try
            {
                var result = await _activityRepository.GetAllActivitiesWithRelations();

                if (result == null || !result.Any())
                    return NotFound("No activities found.");

                var activityDtos = _mapper.Map<List<ActivityDTO>>(result);
                return Ok(activityDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(int id)
        {
            var result = await _activityRepository.GetActivityByIdWithRelations(id);

            if (result == null)
                return NotFound("Activity not found");

            var activityDto = _mapper.Map<ActivityDTO>(result);
            return Ok(activityDto);
        }

        [Authorize]
        [HttpGet("GetUserActivities/{userId}")]
        public async Task<IActionResult> GetActivityByUserId(int userId)
        {
            var result = await _activityRepository.GetAllActivitiesByUserId(userId);

            if (result == null)
                return NotFound("User activity not found");

            var activityDto = _mapper.Map<List<ActivityDTO>>(result);
            return Ok(activityDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityCreateDTO activityCreateDTO)
        {
            if (activityCreateDTO == null)
                return BadRequest("Missing activity data!");

            var activity = new Models.Activity
            {
                Title = activityCreateDTO.Title,
                Description = activityCreateDTO.Description,
                DueDate = activityCreateDTO.DueDate,
                CreatedUserId = activityCreateDTO.CreatedUserId
            };

            var result = await _activityRepository.Create(activity);
            return result;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateActivity([FromBody] ActivityUpdateDto activityUpdateDto)
        {
            if (activityUpdateDto == null)
            {
                return BadRequest("Missing activity data!");
            }

            var result = await _activityRepository.UpdateActivity(activityUpdateDto);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var result = await _activityRepository.Delete(id);
            return result;
        }
    }
}