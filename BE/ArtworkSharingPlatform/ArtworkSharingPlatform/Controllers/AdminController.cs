using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.User;
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
        private readonly IArtworkService _artworkService;
        private readonly ICommissionService _commissionService;
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;
        public AdminController(IUserService userService, IMapper mapper, IReportService reportService, ICommissionService commissionService, IArtworkService artworkService)
        {
            _userService = userService;
            _mapper = mapper;
            _artworkService = artworkService;
            _commissionService = commissionService;
            _reportService = reportService;
        }

        [HttpGet("User")]
        public IActionResult GetAllUser()
        {
            return Ok(_userService.GetAll());
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUserAdmin([FromBody] UserAdminCreateDTO userDto)
        {
            try
            {
                var user = new User
                {
                    Email = userDto.Email,
                    PhoneNumber = userDto.PhoneNumber,
                    Name = userDto.Name,
                    UserName = userDto.Email,
                    Description = userDto.Description,
                    Status = 1,
                    RemainingCredit = 0,
                    PackageId = 0
                };
                await _userService.CreateUserAdmin(user);

                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = userDto.Role
                };

                user.UserRoles = new List<UserRole> { userRole };

                user.UserImage = new UserImage
                {
                    UserId = user.Id,
                    Url = "https://media.istockphoto.com/id/1341046662/vector/picture-profile-icon-human-or-people-sign-and-symbol-for-template-design.jpg?s=612x612&w=0&k=20&c=A7z3OK0fElK3tFntKObma-3a7PyO8_2xxW0jtmjzT78="
                };

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
            catch (Exception ex)
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

        [HttpGet("Artworks")]
        public async Task<IActionResult> GetAllArtwork()
        {
            return Ok(await _artworkService.GetArtworkAdmin());
        }

        [HttpDelete("{artworkId}")]
        public async Task<IActionResult> DeleteArtwork(int artworkId)
        {
            await _artworkService.DeleteArtwork(artworkId);
            return Ok(new { message = "Artwork deleted successfully." });
        }

        [HttpGet("Commissions")]
        public async Task<IActionResult> GetAllCommission()
        {
            return Ok(await _commissionService.GetAllCommissionAdmin());
        }
        [HttpGet("{commissionId}")]
        public async Task<IActionResult> GetSingleCommission(int commissionId)
        {
            return Ok(_commissionService.GetSingleCommission(commissionId));
        }
        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetAllReport()
        {
            var report = await _reportService.GetAllReport();
            return Ok(report);
        }
        [HttpGet("reportDetail")]
        public async Task<ActionResult<ReportDTO>> GetReportDetail(int reportId)
        {
            var report = await _reportService.GetReportById(reportId);
            return Ok(report);
        }

        [HttpPut("report")]
        public async Task<IActionResult> UpdateUserAdmin(int reportId, bool choice)
        {
            //choice: True mean yes, false mean No
            await _reportService.AdminHandleReport(reportId, choice);
            return Ok();
        }

    }
}
