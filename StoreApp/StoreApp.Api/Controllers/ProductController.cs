using Microsoft.AspNetCore.Mvc;
using StoreApp.Dto.GenericResponse;
using StoreApp.Entities;
using StoreApp.Presenters.ViewModels;
using StoreApp.UseCases.Catalog;
using StoreApp.UseCases.Product;

namespace StoreApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly GetAllProductsUseCase<Product, ProductViewModel> _getAllProductsUseCase;
        private readonly GetAllCatalogItemsUseCase<CatalogItemViewModel> _getAllCatalogItemsUseCase;

        public ProductController(GetAllProductsUseCase<Product, ProductViewModel> getAllProductsUseCase, 
            GetAllCatalogItemsUseCase<CatalogItemViewModel> getAllCatalogItemsUseCase)
        {
            _getAllProductsUseCase = getAllProductsUseCase;
            _getAllCatalogItemsUseCase = getAllCatalogItemsUseCase;
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

        [HttpGet]
        [Route("GetAllCatalogItems")]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<CatalogItemViewModel>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<CatalogItemViewModel>>), 404)]
        public async Task<IActionResult> GetAllCatalogItems([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var responseDto = await _getAllCatalogItemsUseCase.ExecuteAsync(pageNumber, pageSize);
            if (responseDto.Data == null || !responseDto.Data.Items.Any())
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }
    }
}
