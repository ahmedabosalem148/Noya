namespace Noya.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ReportDetail> ReportDetails { get; set; }
    }
}
