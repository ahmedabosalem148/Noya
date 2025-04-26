using System.Data;

namespace Noya.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ImageUrl { get; set; }

        // Related collections
        public List<Role> Roles { get; set; }
        public List<Order> Orders { get; set; }
        public List<Report> Reports { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
