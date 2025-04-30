using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class WorkerRepository
    {
        public IEnumerable<Worker> GetAll()
        {
            var workers = new List<Worker>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Workers", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workers.Add(new Worker
                        {
                            WorkerId = reader.GetInt32(reader.GetOrdinal("worker_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Phone = reader.GetString(reader.GetOrdinal("phone")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Specialization = reader.GetString(reader.GetOrdinal("specialization")),
                            ExperienceYears = reader.GetInt32(reader.GetOrdinal("experience_years")),
                            Location = reader.GetString(reader.GetOrdinal("location")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url"))
                        });
                    }
                }
            }
            return workers;
        }

        public Worker GetById(int id)
        {
            Worker worker = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Workers WHERE worker_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        worker = new Worker
                        {
                            WorkerId = reader.GetInt32(reader.GetOrdinal("worker_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Phone = reader.GetString(reader.GetOrdinal("phone")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Specialization = reader.GetString(reader.GetOrdinal("specialization")),
                            ExperienceYears = reader.GetInt32(reader.GetOrdinal("experience_years")),
                            Location = reader.GetString(reader.GetOrdinal("location")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url"))
                        };
                    }
                }
            }
            return worker;
        }

        public void Add(Worker worker)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    INSERT INTO Workers (name, phone, email, specialization, experience_years, location, image_url)
                    VALUES (@Name, @Phone, @Email, @Specialization, @ExperienceYears, @Location, @ImageUrl)", conn);

                cmd.Parameters.AddWithValue("@Name", worker.Name);
                cmd.Parameters.AddWithValue("@Phone", worker.Phone);
                cmd.Parameters.AddWithValue("@Email", worker.Email);
                cmd.Parameters.AddWithValue("@Specialization", worker.Specialization);
                cmd.Parameters.AddWithValue("@ExperienceYears", worker.ExperienceYears);
                cmd.Parameters.AddWithValue("@Location", worker.Location);
                cmd.Parameters.AddWithValue("@ImageUrl", worker.ImageUrl);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Worker worker)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    UPDATE Workers SET 
                        name = @Name,
                        phone = @Phone,
                        email = @Email,
                        specialization = @Specialization,
                        experience_years = @ExperienceYears,
                        location = @Location,
                        image_url = @ImageUrl
                    WHERE worker_id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", worker.WorkerId);
                cmd.Parameters.AddWithValue("@Name", worker.Name);
                cmd.Parameters.AddWithValue("@Phone", worker.Phone);
                cmd.Parameters.AddWithValue("@Email", worker.Email);
                cmd.Parameters.AddWithValue("@Specialization", worker.Specialization);
                cmd.Parameters.AddWithValue("@ExperienceYears", worker.ExperienceYears);
                cmd.Parameters.AddWithValue("@Location", worker.Location);
                cmd.Parameters.AddWithValue("@ImageUrl", worker.ImageUrl);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Workers WHERE worker_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
