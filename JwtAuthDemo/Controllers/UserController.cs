using JwtAuthDemo.Models;
using JwtAuthDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving s: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }
            try
            {
                var createdUser = _userService.AddUser(user);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating new user: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || user.Id != id)
            {
                return BadRequest("User data is null or ID mismatch.");
            }
            try
            {
                var updatedUser = _userService.UpdateUser(id,user);
                if (updatedUser == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating user: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting user: {ex.Message}");
            }
        }
    }
}