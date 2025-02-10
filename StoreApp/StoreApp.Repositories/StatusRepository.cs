using StoreApp.Entities;
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
        public Task<Status> AddAsync(Status entity)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Status>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Status> UpdateAsync(Status entity)
        {
            throw new NotImplementedException();
        }
    }
}
