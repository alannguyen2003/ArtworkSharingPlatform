using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.Domain.Entities.Users;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharingHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AdminController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("User")]
        public IActionResult GetAllUser()
        {
            return Ok(_userService.GetAll());
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUserAdmin([FromBody] UserAdminDTO userDto)
        {
            try
            {
                var user = new User
                {
                    Email = userDto.Email,
                    PhoneNumber = userDto.PhoneNumber,
                    Name = userDto.Name,
                    UserName = userDto.Email,
                    Status = userDto.Status,
                    RemainingCredit = userDto.RemaningCredit,
                    PackageId = userDto.PackageId
                };
                await _userService.CreateUserAdmin(user);

                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = userDto.Role
                };

                user.UserRoles = new List<UserRole> { userRole };

                await _userService.UpdateUserAdmin(user);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var userDto = _userService.GetUserById(id);
                if (userDto == null)
                {
                    return NotFound("User not found");
                }

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUserAdmin([FromBody] UserAdminDTO userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                await _userService.UpdateUserAdmin(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserAdmin([FromBody] UserAdminDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userService.DeleteUserAdmin(user);
            return Ok();
        }
    }
}
