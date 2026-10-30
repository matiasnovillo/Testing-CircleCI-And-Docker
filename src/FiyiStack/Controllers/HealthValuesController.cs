using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiyiStack.Controllers
{
    [ApiController]
    public class HealthValuesController : ControllerBase
    {
        [HttpGet("~/api/health")]
        public IActionResult GetHealth()
        {
            var response = new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow.ToString("o")
            };

            return Ok(response);
        }
    }
}
