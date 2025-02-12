using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Entities;
using StoreApp.Models;
using StoreApp.UseCases.Interfaces;

namespace StoreApp.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Product entity)
        {
            var Product = _mapper.Map<ProductModel>(entity);
            await _dbContext.Products.AddAsync(Product);
            int createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int removedElements = 0;
            var Product = await _dbContext.Products
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (Product != null)
            {
                _dbContext.Products.Remove(Product);
                removedElements = await _dbContext.SaveChangesAsync();
            }
            return removedElements;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products
                .ProjectTo<Product>(_mapper.ConfigurationProvider) // Optimize mapping in DB (Using IQueryable)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _dbContext.Products
                .Where(c => c.Id == id)
                .ProjectTo<Product>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            int updatedElements = 0;
            var Product = await _dbContext.Products
                .Where(c => c.Id == entity.Id)
                .FirstOrDefaultAsync();
            if (Product != null)
            {
                _mapper.Map(entity, Product);
                updatedElements = await _dbContext.SaveChangesAsync();
            }
            return updatedElements;
        }
    }
}
