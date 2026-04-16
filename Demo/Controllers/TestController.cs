using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]   
    [ApiController]               
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet("auth")]
        public IActionResult Auth()
        {
            return Ok("Authenticated");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return Ok("Admin only");
        }
    }
}

//Ceation of web api controller with two endpoints, one for authenticated users and another for users with the "Admin" role. The [Authorize] attribute is used to enforce authentication and role-based authorization.