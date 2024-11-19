using ActivityManagerAPI.Models;
using ActivityManagerAPI.Models.Dtos;
using ActivityManagerAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivitiesController : ControllerBase
    {
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IActivityRepository _activityRepository;

        public UserActivitiesController(
            IUserActivityRepository userActivityRepository,
            IUserRepository userRepository,
            IActivityRepository activityRepository)
        {
            _userActivityRepository = userActivityRepository;
            _userRepository = userRepository;
            _activityRepository = activityRepository;
        }

        [HttpPost("AssignActivityToUser")]
        public async Task<IActionResult> AssignActivityToUser([FromBody] UserActivityAssignDTO userActivityDTO)
        {
            if (userActivityDTO == null)
                return BadRequest("Missing data!");

            var userExists = await _userRepository.GetById(userActivityDTO.UserId) != null;
            var activityExists = await _activityRepository.GetById(userActivityDTO.ActivityId) != null;

            if (!userExists || !activityExists)
                return NotFound("User or Activity not found");

            try
            {
                var userActivity = new UserActivity
                {
                    UserId = userActivityDTO.UserId,
                    ActivityId = userActivityDTO.ActivityId,
                    AssignerUserId = userActivityDTO.AssignerUserId,
                    IsCompleted = false
                };

                await _userActivityRepository.Create(userActivity);

                return CreatedAtAction(nameof(GetUserActivity), new { userId = userActivity.UserId, activityId = userActivity.ActivityId }, userActivity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetUserActivity/{userId}/{activityId}")]
        public async Task<IActionResult> GetUserActivity(int userId, int activityId)
        {
            var userActivity = await _userActivityRepository.GetUserActivityByUserAndActivityDTO(userId, activityId);

            if (userActivity == null)
                return NotFound("UserActivity not found");

            return Ok(userActivity);
        }

        [HttpPut("UpdateUserActivityStatus/{userId}/{activityId}")]
        public async Task<IActionResult> UpdateUserActivityStatus(int userId, int activityId, bool isCompleted)
        {
            var userActivity = await _userActivityRepository.GetUserActivityByUserAndActivity(userId, activityId);

            if (userActivity == null)
                return NotFound("User activity not found");

            userActivity.IsCompleted = isCompleted;

            try
            {
                await _userActivityRepository.Update(userActivity);
                return Ok(userActivity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("UnassignUserActivity/{userId}/{activityId}")]
        public async Task<IActionResult> UnassignUserActivity(int userId, int activityId)
        {
            var userActivity = await _userActivityRepository.GetUserActivityByUserAndActivity(userId, activityId);

            if (userActivity == null)
                return NotFound("UserActivity not found");

            try
            {
                await _userActivityRepository.Delete(userActivity.Id);
                return Ok("Activity unassigned");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
