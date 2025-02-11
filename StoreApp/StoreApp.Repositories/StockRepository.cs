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
    public class StockRepository : IRepository<Stock>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public StockRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Stock entity)
        {
            var Stock = _mapper.Map<StockModel>(entity);
            await _dbContext.Stocks.AddAsync(Stock);
            int createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int removedElements = 0;
            var Stock = await _dbContext.Stocks
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (Stock != null)
            {
                _dbContext.Stocks.Remove(Stock);
                removedElements = await _dbContext.SaveChangesAsync();
            }
            return removedElements;
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _dbContext.Stocks
                .ProjectTo<Stock>(_mapper.ConfigurationProvider) // Optimize mapping in DB (Using IQueryable)
                .ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _dbContext.Stocks
                .Where(c => c.Id == id)
                .ProjectTo<Stock>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Stock entity)
        {
            int updatedElements = 0;
            var Stock = await _dbContext.Stocks
                .Where(c => c.Id == entity.Id)
                .FirstOrDefaultAsync();
            if (Stock != null)
            {
                _mapper.Map(entity, Stock);
                updatedElements = await _dbContext.SaveChangesAsync();
            }
            return updatedElements;
        }
    }
}
