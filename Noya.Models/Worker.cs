using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noya.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public int ExperienceYears { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }

        public List<WorkerJob> Jobs { get; set; }
    }
}
