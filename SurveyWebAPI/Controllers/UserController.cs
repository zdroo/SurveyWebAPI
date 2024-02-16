using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public List<User> users = new List<User>();

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            DataAccess db = new DataAccess();
            users = await db.GetUsersAsync();
            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult<List<User>>> PostUser(string name, int age)
        {
            DataAccess db = new DataAccess();
            await db.AddUserAsync(name, age);
            users = await db.GetUsersAsync();
            return Ok(users);
        }
    }
}
