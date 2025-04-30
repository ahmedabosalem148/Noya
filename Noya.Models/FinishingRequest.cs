using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noya.Models
{
    public class FinishingRequest
    {
        public int Id { get; set; }
        public int Area { get; set; }
        public decimal Budget { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
