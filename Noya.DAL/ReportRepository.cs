using System;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class ReportRepository
    {
        public void AddFinishingRequest(int userId, int area, decimal budget, string location)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // إضافة التقرير الرئيسي
                var reportCmd = new SqlCommand(@"
                    INSERT INTO Reports (user_id, status, report_type, created_at)
                    VALUES (@UserId, 'Open', 'FinishingRequest', GETDATE());
                    SELECT SCOPE_IDENTITY();", conn);

                reportCmd.Parameters.AddWithValue("@UserId", userId);
                int reportId = Convert.ToInt32(reportCmd.ExecuteScalar());

                // إضافة تفاصيل التقرير
                var detailCmd = new SqlCommand(@"
                    INSERT INTO Report_Details (report_id, description, area, budget, location, updated_at)
                    VALUES (@ReportId, @Description, @Area, @Budget, @Location, GETDATE());", conn);

                detailCmd.Parameters.AddWithValue("@ReportId", reportId);
                detailCmd.Parameters.AddWithValue("@Description", "طلب تشطيب شقة");
                detailCmd.Parameters.AddWithValue("@Area", area);
                detailCmd.Parameters.AddWithValue("@Budget", budget);
                detailCmd.Parameters.AddWithValue("@Location", location);

                detailCmd.ExecuteNonQuery();
            }
        }
    }
}
