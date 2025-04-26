namespace Noya.Models
{
    public class ReportDetail
    {
        public int ReportDetailId { get; set; }
        public int ReportId { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
