namespace Noya.Models
{
    public class Bundle
    {
        public int BundleId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public List<BundleItem> BundleItems { get; set; }
    }
}
