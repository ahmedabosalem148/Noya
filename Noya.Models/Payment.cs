namespace Noya.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionStatus { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
