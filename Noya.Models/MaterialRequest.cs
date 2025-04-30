using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noya.Models
{
    public class MaterialRequest
    {
        public int RequestId { get; set; }
        public int MaterialId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
