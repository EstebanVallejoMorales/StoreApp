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
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderItemRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(OrderItem entity)
        {
            var OrderItem = _mapper.Map<OrderItemModel>(entity);
            await _dbContext.OrderItems.AddAsync(OrderItem);
            int createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int removedElements = 0;
            var OrderItem = await _dbContext.OrderItems
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (OrderItem != null)
            {
                _dbContext.OrderItems.Remove(OrderItem);
                removedElements = await _dbContext.SaveChangesAsync();
            }
            return removedElements;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _dbContext.OrderItems
                .ProjectTo<OrderItem>(_mapper.ConfigurationProvider) // Optimize mapping in DB (Using IQueryable)
                .ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _dbContext.OrderItems
                .Where(c => c.Id == id)
                .ProjectTo<OrderItem>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(OrderItem entity)
        {
            int updatedElements = 0;
            var OrderItem = await _dbContext.OrderItems
                .Where(c => c.Id == entity.Id)
                .FirstOrDefaultAsync();
            if (OrderItem != null)
            {
                _mapper.Map(entity, OrderItem);
                updatedElements = await _dbContext.SaveChangesAsync();
            }
            return updatedElements;
        }
    }
}
