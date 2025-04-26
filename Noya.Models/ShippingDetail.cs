namespace Noya.Models
{
    public class ShippingDetail
    {
        public int ShippingId { get; set; }
        public int OrderId { get; set; }
        public int? AgencyId { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
    }
}
