using StoreApp.Dto.GenericResponse;
using StoreApp.UseCases.Interfaces;

namespace StoreApp.UseCases.Catalog
{
    public class GetAllCatalogItemsUseCase<TOutput>
    {
        private readonly ICatalogItemRepository<TOutput> _catalogItemRepository;
        private readonly IPresenter<TOutput, TOutput> _presenter;

        public GetAllCatalogItemsUseCase(ICatalogItemRepository<TOutput> catalogItemRepository,
            IPresenter<TOutput, TOutput> presenter)
        {
            _catalogItemRepository = catalogItemRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<PagedResultDto<TOutput>>> ExecuteAsync(int pageNumber, int pageSize)
        {
            ResponseDto<PagedResultDto<TOutput>> responseDto = new();
            try
            {
                var pagedItems = await _catalogItemRepository.GetAllCatalogItemsAsync(pageNumber, pageSize);

                var catalogItemsViewModel = _presenter.Present(pagedItems.Items);
                pagedItems.Items = catalogItemsViewModel;

                responseDto.Data = pagedItems;
                if (!responseDto.Data.Items.Any())
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
