using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Dto.GenericResponse;
using StoreApp.Presenters.ViewModels;
using StoreApp.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository<CatalogItemViewModel>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CatalogItemRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<CatalogItemViewModel>> GetAllCatalogItemsAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.Products
                .ProjectTo<CatalogItemViewModel>(_mapper.ConfigurationProvider);

            Task<int> totalCountTask = query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<CatalogItemViewModel>(items, await totalCountTask, pageNumber, pageSize);
        }
    }
}
