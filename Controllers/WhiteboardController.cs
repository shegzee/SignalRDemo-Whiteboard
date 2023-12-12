using Microsoft.AspNetCore.Mvc;

namespace SignalRDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhiteboardController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Collaborative Whiteboard API is running at /whiteboardHub!");
        }

    }
}
