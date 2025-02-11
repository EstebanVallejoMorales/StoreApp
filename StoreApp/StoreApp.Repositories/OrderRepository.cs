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
    public class OrderRepository : IRepository<Order>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Order entity)
        {
            var Order = _mapper.Map<OrderModel>(entity);
            await _dbContext.Orders.AddAsync(Order);
            int createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int removedElements = 0;
            var Order = await _dbContext.Orders
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (Order != null)
            {
                _dbContext.Orders.Remove(Order);
                removedElements = await _dbContext.SaveChangesAsync();
            }
            return removedElements;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbContext.Orders
                .ProjectTo<Order>(_mapper.ConfigurationProvider) // Optimize mapping in DB (Using IQueryable)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _dbContext.Orders
                .Where(c => c.Id == id)
                .ProjectTo<Order>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Order entity)
        {
            int updatedElements = 0;
            var Order = await _dbContext.Orders
                .Where(c => c.Id == entity.Id)
                .FirstOrDefaultAsync();
            if (Order != null)
            {
                _mapper.Map(entity, Order);
                updatedElements = await _dbContext.SaveChangesAsync();
            }
            return updatedElements;
        }
    }
}
