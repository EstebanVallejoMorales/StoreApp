using StoreApp.Entities;
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
        public Task<ProductCategory> AddAsync(ProductCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> UpdateAsync(ProductCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
