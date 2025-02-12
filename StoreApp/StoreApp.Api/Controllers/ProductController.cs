using Microsoft.AspNetCore.Mvc;
using StoreApp.Dto.GenericResponse;
using StoreApp.Entities;
using StoreApp.Presenters.ViewModels;
using StoreApp.UseCases.Catalog;

namespace StoreApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly GetAllProductsUseCase<Product, ProductViewModel> _getAllProductsUseCase;

        public ProductController(GetAllProductsUseCase<Product, ProductViewModel> getAllProductsUseCase)
        {
            _getAllProductsUseCase = getAllProductsUseCase;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<ProductViewModel>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<ProductViewModel>>), 404)]
        public async Task<IActionResult> GetAllStudents()
        {
            var responseDto = await _getAllProductsUseCase.ExecuteAsync();
            if (responseDto.Data == null || !responseDto.Data.Any())
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }
    }
}
