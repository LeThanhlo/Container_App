using Container_App.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Container_App.Model;
using Container_App.utilities;

namespace Container_App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("get-all-user")]
        public async Task<ActionResult<List<User>>> GetUsers(PagedResult page)
        {
            var users = await _userService.GetUsers(page);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("get-user-by-id/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [Authorize]
        [HttpPost("create-user")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var createdUser = await _userService.CreateUser(user);

            var response = new ResponseModel(
                success: true,
                message: "Thêm user thành công!",
                data: createdUser
            );

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, response);
        }

        [Authorize]
        [HttpPut("update-user/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            var updatedUser = await _userService.UpdateUser(id, user);

            if (updatedUser == null)
                return NotFound(new ResponseModel(false, "Không tìm thấy user!"));

            var response = new ResponseModel(
                success: true,
                message: "Cập nhật user thành công!",
                data: updatedUser
            );

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            var response = new ResponseModel(
                success: true,
                message: "Xoá user thành công!"
            );

            return Ok(response);
        }
    }
}
