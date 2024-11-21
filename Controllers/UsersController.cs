using ActivityManagerAPI.Data;
using ActivityManagerAPI.Helpers;
using ActivityManagerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ActivityManagerAPI.Models.Dtos;
using ActivityManagerAPI.Repositories.Concrete;
using ActivityManagerAPI.Repositories.Abstract;
using AutoMapper;
using ActivityManagerAPI.Models.Abstract;

namespace ActivityManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(AppDbContext context, IUserRepository userRepository, IMapper mapper)
        {
            _context = context;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await _userRepository.GetAll();

                if (result is OkObjectResult okResult)
                {
                    var entities = okResult.Value as List<User>;

                    if (entities == null || !entities.Any())
                        return NotFound("No users found.");

                    var usersDto = _mapper.Map<List<UserDTO>>(entities);
                    return Ok(usersDto);
                }

                if (result is NotFoundResult)
                    return NotFound("No users found.");

                return StatusCode(500, "Unexpected error occurred.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
