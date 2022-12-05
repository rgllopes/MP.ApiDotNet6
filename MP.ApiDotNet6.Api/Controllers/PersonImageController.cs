using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs.PersonImage;
using MP.ApiDotNet6.Application.Services.Interface;

namespace MP.ApiDotNet6.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonImageController : ControllerBase
    {
        private readonly IPersonImageService _personImageService;

        public PersonImageController(IPersonImageService personImageService)
        {
            _personImageService = personImageService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveImage(PersonImageDTO personImageDTO)
        {
            var result = await _personImageService.CreateImageBase64Async(personImageDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
