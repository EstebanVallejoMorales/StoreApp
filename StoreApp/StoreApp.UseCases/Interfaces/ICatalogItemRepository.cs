using StoreApp.Dto.GenericResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.UseCases.Interfaces
{
    public interface ICatalogItemRepository<TEntity>
    {
        Task<PagedResultDto<TEntity>> GetAllCatalogItemsAsync(int pageNumber, int pageSize);
    }
}
