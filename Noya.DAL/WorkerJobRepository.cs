using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class WorkerJobRepository
    {
        public IEnumerable<WorkerJob> GetByWorkerId(int workerId)
        {
            var jobs = new List<WorkerJob>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Worker_Jobs WHERE worker_id = @WorkerId", conn);
                cmd.Parameters.AddWithValue("@WorkerId", workerId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jobs.Add(new WorkerJob
                        {
                            JobId = reader.GetInt32(reader.GetOrdinal("job_id")),
                            WorkerId = reader.GetInt32(reader.GetOrdinal("worker_id")),
                            JobTitle = reader.GetString(reader.GetOrdinal("job_title")),
                            JobDescription = reader.GetString(reader.GetOrdinal("job_description")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"))
                        });
                    }
                }
            }
            return jobs;
        }

        public void Add(WorkerJob job)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    INSERT INTO Worker_Jobs (worker_id, job_title, job_description, status)
                    VALUES (@WorkerId, @JobTitle, @JobDescription, @Status)", conn);

                cmd.Parameters.AddWithValue("@WorkerId", job.WorkerId);
                cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                cmd.Parameters.AddWithValue("@Status", job.Status);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(WorkerJob job)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    UPDATE Worker_Jobs 
                    SET job_title = @JobTitle, job_description = @JobDescription, status = @Status
                    WHERE job_id = @JobId", conn);

                cmd.Parameters.AddWithValue("@JobId", job.JobId);
                cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                cmd.Parameters.AddWithValue("@Status", job.Status);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int jobId)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Worker_Jobs WHERE job_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", jobId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
