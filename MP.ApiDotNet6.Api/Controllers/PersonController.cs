using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs.Person;
using MP.ApiDotNet6.Application.Services.Interface;
using MP.ApiDotNet6.Domain.FiltersDb;

namespace MP.ApiDotNet6.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PersonDTO personDTO)
        {
            var result = await _personService.CreateAsync(personDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _personService.GetAsync();
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _personService.GetByIsAsync(id);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] PersonDTO personDTO)
        {
            var result = await _personService.UpdateAsync(personDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DleteAsync(int id)
        {
            var result = await _personService.DeleteAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{paged}")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] PersonFilterDb personFilterDb)
        {
            var result = await _personService.GetPagedAsync(personFilterDb);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
