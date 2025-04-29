namespace Noya.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        // 👇 Add this line
        public List<Product> Products { get; set; }
    }

}
