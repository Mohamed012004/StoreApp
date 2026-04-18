using Microsoft.AspNetCore.Mvc;

namespace Store.Route.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")] // GET: baseUrl/api/buggy/notfound
        public IActionResult GetNotFoundResponse()
        {
            // lpgic
            return NotFound();

        }

        [HttpGet("badrequst")] // GET: baseUrl/api/buggy/badrequst
        public IActionResult GetBadRequestResponse()
        {
            // lpgic
            return BadRequest();

        }

        [HttpGet("badrequst{id}")] // GET: baseUrl/api/buggy/badrequst{id}
        public IActionResult GetValidationErrorResponse(int id)
        {
            // lpgic
            return BadRequest();

        }

        [HttpGet("servererror")] // GET: baseUrl/api/buggy/servererror
        public IActionResult GetServerErrorResponse()
        {
            // lopgic
            throw new Exception();
            return BadRequest();

        }

        [HttpGet("unauthorized")] // GET: baseUrl/api/buggy/badrequst{id}
        public IActionResult GetUnAuthorizedResponse()
        {
            // lpgic

            return Unauthorized();

        }



    }
}
