using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs.Product;
using MP.ApiDotNet6.Application.Services.Interface;

namespace MP.ApiDotNet6.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) 
        { 
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.CreateAsync(productDTO);
            if(result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _productService.GetAsync();
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdSync(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if(result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.UpdateAsync(productDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> PostAsync(int id)
        {
            var result = await _productService.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
