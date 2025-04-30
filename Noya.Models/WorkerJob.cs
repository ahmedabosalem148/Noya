using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noya.Models
{
    public class WorkerJob
    {
        public int JobId { get; set; }
        public int WorkerId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
