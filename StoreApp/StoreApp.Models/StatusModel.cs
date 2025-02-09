using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Models
{
    public class StatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<OrderModel> OrderModels { get; set; }
    }
}
