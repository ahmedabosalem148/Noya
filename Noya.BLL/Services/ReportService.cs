using Noya.DAL;
using Noya.Models;

namespace Noya.BLL
{
    public class ReportService
    {
        private readonly ReportRepository _reportRepository;

        public ReportService()
        {
            _reportRepository = new ReportRepository();
        }

        public void CreateFinishingRequest(int userId, int area, decimal budget, string address)
        {
            _reportRepository.AddFinishingRequest(userId, area, budget, address);
        }

        // يمكن إضافة المزيد من الوظائف هنا، مثل:
        // - GetReportsByUserId
        // - UpdateReportStatus
        // - DeleteReport
        // - وغيرها حسب الحاجة
    }
}
