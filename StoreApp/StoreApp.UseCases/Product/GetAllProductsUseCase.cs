using StoreApp.Dto.GenericResponse;
using StoreApp.UseCases.Interfaces;

namespace StoreApp.UseCases.Product
{
    public class GetAllProductsUseCase<TInputEntity, TOutput>
    {
        private readonly IRepository<TInputEntity> _productRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetAllProductsUseCase(IRepository<TInputEntity> productRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _productRepository = productRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<IEnumerable<TOutput>>> ExecuteAsync()
        {
            ResponseDto<IEnumerable<TOutput>> responseDto = new ResponseDto<IEnumerable<TOutput>>();
            try
            {
                var products = await _productRepository.GetAllAsync();

                var productsViewModel = _presenter.Present(products);
                responseDto.Data = productsViewModel;
                if (!responseDto.Data.Any())
                {
                    responseDto.Message = "No se encontraron productos.";
                }
                else
                {
                    responseDto.Message = "Productos cargados exitosamente";
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener los productos"
                });
            }
            return responseDto;
        }
    }
}
