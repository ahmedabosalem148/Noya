using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noya.Models
{
    public class Material
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int? ProviderId { get; set; }
        public string ImageUrl { get; set; }
    }
}

