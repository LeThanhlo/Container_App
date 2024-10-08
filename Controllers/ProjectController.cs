using Container_App.Model;
using Container_App.Services.ProjectService;
using Container_App.utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Container_App.Controllers
{
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize] // Yêu cầu xác thực
        [HttpPost("create-project")]
        public async Task<ActionResult<Project>> CreateProject([FromBody] Project project)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); // Lấy userId từ token

            try
            {
                var createdProject = await _projectService.CreateProjectAsync(project, userId);
                return CreatedAtAction(nameof(CreateProject), new { id = createdProject.ProjectId }, createdProject);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message); // Trả về lỗi 403 nếu không đủ quyền
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Trả về lỗi 500 cho các lỗi khác
            }
        }

        [HttpPost("invite-user")]
        public async Task<ActionResult> SendInvite([FromBody] ProjectUserInvite invite)
        {
            try
            {
                await _projectService.SendInviteAsync(invite);
                return Ok("Lời mời đã được gửi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
