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
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Customer entity)
        {
            var Customer = _mapper.Map<CustomerModel>(entity);
            await _dbContext.Customers.AddAsync(Customer);
            int createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int removedElements = 0;
            var Customer = await _dbContext.Customers
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (Customer != null)
            {
                _dbContext.Customers.Remove(Customer);
                removedElements = await _dbContext.SaveChangesAsync();
            }
            return removedElements;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbContext.Customers
                .ProjectTo<Customer>(_mapper.ConfigurationProvider) // Optimize mapping in DB (Using IQueryable)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _dbContext.Customers
                .Where(c => c.Id == id)
                .ProjectTo<Customer>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Customer entity)
        {
            int updatedElements = 0;
            var Customer = await _dbContext.Customers
                .Where(c => c.Id == entity.Id)
                .FirstOrDefaultAsync();
            if (Customer != null)
            {
                _mapper.Map(entity, Customer);
                updatedElements = await _dbContext.SaveChangesAsync();
            }
            return updatedElements;
        }
    }
}
