using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowershopAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSecret()
        {
            return Ok("You are authenticated!");
        }
        [HttpGet("other")]
        public IActionResult GetOtherSecret()
        {
            return Ok("You got the other secret");
        }
    }
}
