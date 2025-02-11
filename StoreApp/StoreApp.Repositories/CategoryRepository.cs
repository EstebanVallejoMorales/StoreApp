using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Entities;
using StoreApp.Models;
using StoreApp.UseCases.Interfaces;
using System.Reflection;

namespace StoreApp.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Category entity)
        {
            var category = _mapper.Map<CategoryModel>(entity);
            await _dbContext.Categories.AddAsync(category);
            int createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int removedElements = 0;
            var category = await _dbContext.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                removedElements = await _dbContext.SaveChangesAsync();
            }           
            return removedElements;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories
                .ProjectTo<Category>(_mapper.ConfigurationProvider) // Optimize mapping in DB (Using IQueryable)
                .ToListAsync();            
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbContext.Categories
                .Where(c => c.Id == id)
                .ProjectTo<Category>(_mapper.ConfigurationProvider) 
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Category entity)
        {
            int updatedElements = 0;
            var category = await _dbContext.Categories
                .Where(c => c.Id == entity.Id)
                .FirstOrDefaultAsync();
            if (category != null)
            {
                _mapper.Map(entity, category);
                updatedElements = await _dbContext.SaveChangesAsync();
            }
            return updatedElements;
        }
    }
}
