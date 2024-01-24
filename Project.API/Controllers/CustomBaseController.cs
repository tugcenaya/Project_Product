using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTOs;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        // This method creates an IActionResult based on the provided CustomResponseDto
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            // If the response status code is 204, return an ObjectResult with null content
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null) { StatusCode = response.StatusCode };
            }

            // Otherwise, return an ObjectResult with the response as content
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}