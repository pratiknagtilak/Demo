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