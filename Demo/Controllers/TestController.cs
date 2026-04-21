using Demo.Exceptions;
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
        [HttpGet("crash")]
        public IActionResult Crash()
        {
            throw new Exception("Test server crash");
        }

        
        [HttpGet("notfound")]
        public IActionResult NotFoundTest()
        {
            throw new NotFoundException("Item not found");
        }

        
        [HttpGet("validation")]
        public IActionResult ValidationTest()
        {
            throw new ValidationException("Invalid input provided");
        }

        
        [HttpGet("null")]
        public IActionResult NullTest()
        {
            string? value = null;
            return Ok(value.Length); 
        }
    }
}

//Ceation of web api controller with two endpoints, one for authenticated users and another for users with the "Admin" role. The [Authorize] attribute is used to enforce authentication and role-based authorization.