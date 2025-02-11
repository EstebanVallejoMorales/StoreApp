using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Entities;
using StoreApp.Models;
using StoreApp.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Repositories
{
    public class ProductCategoryRepository : IRepository<ProductCategory>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductCategoryRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(ProductCategory entity)
        {
            var ProductCategory = _mapper.Map<ProductCategoryModel>(entity);
            await _dbContext.ProductCategories.AddAsync(ProductCategory);
            int createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int removedElements = 0;
            var ProductCategory = await _dbContext.ProductCategories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (ProductCategory != null)
            {
                _dbContext.ProductCategories.Remove(ProductCategory);
                removedElements = await _dbContext.SaveChangesAsync();
            }
            return removedElements;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _dbContext.ProductCategories
                .ProjectTo<ProductCategory>(_mapper.ConfigurationProvider) // Optimize mapping in DB (Using IQueryable)
                .ToListAsync();
        }

        public async Task<ProductCategory?> GetByIdAsync(int id)
        {
            return await _dbContext.ProductCategories
                .Where(c => c.Id == id)
                .ProjectTo<ProductCategory>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(ProductCategory entity)
        {
            int updatedElements = 0;
            var ProductCategory = await _dbContext.ProductCategories
                .Where(c => c.Id == entity.Id)
                .FirstOrDefaultAsync();
            if (ProductCategory != null)
            {
                _mapper.Map(entity, ProductCategory);
                updatedElements = await _dbContext.SaveChangesAsync();
            }
            return updatedElements;
        }
    }
}
