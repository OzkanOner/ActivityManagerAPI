using ActivityManagerAPI.Models;
using ActivityManagerAPI.Models.Dtos;
using ActivityManagerAPI.Repositories.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(int id)
        {
            var result = await _activityRepository.GetActivityByIdWithRelations(id);

            if (result == null)
                return NotFound("Activity not found");

            var activityDto = _mapper.Map<ActivityDTO>(result);
            return Ok(activityDto);
        }

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

        [HttpPut]
        public async Task<IActionResult> UpdateActivity([FromBody] Models.Activity activity)
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