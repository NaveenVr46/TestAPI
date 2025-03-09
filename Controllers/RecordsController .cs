using Microsoft.AspNetCore.Mvc;
using TestAPI.BAL.Interface;
using TestAPI.DAL;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordsController : Controller
    {
        private readonly IRecordRepository _repository;

        public RecordsController(IRecordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<Record>>> GetUsers()
        {
            var records = await _repository.GetUserAsync();
            return Ok(records);
        }

        [HttpPost("InsertUser")]
        public async Task<IActionResult> InsertUser(Record user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            var result = await _repository.InsertUserAsync(user);

            if (result > 0)
            {
                return Ok(new { message = "User added successfully" });
            }
            else
            {
                return StatusCode(500, "Error inserting user.");
            }
        }

        //[HttpPut("UpdateUser/{id}")]
        //public async Task<IActionResult> UpdateUser(int id, [FromBody] Record user)
        //{
        //    if (user == null || id != user.Id)
        //    {
        //        return BadRequest("Invalid user data.");
        //    }

        //    var result = await _repository.UpdateUserAsync(user);

        //    if (result > 0)
        //    {
        //        return Ok(new { message = "User updated successfully" });
        //    }
        //    else
        //    {
        //        return NotFound("User not found.");
        //    }
        //}

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<Record>> GetUserById(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Record user)
        {

            var updated = await _repository.UpdateUserAsync(user);

            if (updated == 1)
            {
                return Ok(new { message = "User updated successfully." });
            }
            else
            {
                return NotFound(new { message = "User not found or update failed." });
            }
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _repository.DeleteUserAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}