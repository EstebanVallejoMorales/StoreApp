using StoreApp.Entities;
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
        public Task<Stock> AddAsync(Stock entity)
        {
            throw new NotImplementedException();
        }

        public Task<Stock> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Stock> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Stock> UpdateAsync(Stock entity)
        {
            throw new NotImplementedException();
        }
    }
}
