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
    public class StatusRepository : IRepository<Status>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public StatusRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Status entity)
        {
            var Status = _mapper.Map<StatusModel>(entity);
            await _dbContext.Statuses.AddAsync(Status);
            int createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int removedElements = 0;
            var Status = await _dbContext.Statuses
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (Status != null)
            {
                _dbContext.Statuses.Remove(Status);
                removedElements = await _dbContext.SaveChangesAsync();
            }
            return removedElements;
        }

        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _dbContext.Statuses
                .ProjectTo<Status>(_mapper.ConfigurationProvider) // Optimize mapping in DB (Using IQueryable)
                .ToListAsync();
        }

        public async Task<Status?> GetByIdAsync(int id)
        {
            return await _dbContext.Statuses
                .Where(c => c.Id == id)
                .ProjectTo<Status>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Status entity)
        {
            int updatedElements = 0;
            var Status = await _dbContext.Statuses
                .Where(c => c.Id == entity.Id)
                .FirstOrDefaultAsync();
            if (Status != null)
            {
                _mapper.Map(entity, Status);
                updatedElements = await _dbContext.SaveChangesAsync();
            }
            return updatedElements;
        }
    }
}
