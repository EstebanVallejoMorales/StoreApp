using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Models
{
    public class ProductCategoryModel
    {
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
