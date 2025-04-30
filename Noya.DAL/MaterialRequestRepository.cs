using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class MaterialRequestRepository
    {
        public IEnumerable<MaterialRequest> GetAll()
        {
            var requests = new List<MaterialRequest>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Material_Requests", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requests.Add(new MaterialRequest
                        {
                            RequestId = reader.GetInt32(reader.GetOrdinal("request_id")),
                            MaterialId = reader.GetInt32(reader.GetOrdinal("material_id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                            RequestDate = reader.GetDateTime(reader.GetOrdinal("request_date"))
                        });
                    }
                }
            }
            return requests;
        }

        public MaterialRequest GetById(int id)
        {
            MaterialRequest request = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Material_Requests WHERE request_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        request = new MaterialRequest
                        {
                            RequestId = reader.GetInt32(reader.GetOrdinal("request_id")),
                            MaterialId = reader.GetInt32(reader.GetOrdinal("material_id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                            RequestDate = reader.GetDateTime(reader.GetOrdinal("request_date"))
                        };
                    }
                }
            }
            return request;
        }

        public void Add(MaterialRequest request)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    INSERT INTO Material_Requests (material_id, user_id, quantity, status)
                    VALUES (@MaterialId, @UserId, @Quantity, @Status)", conn);

                cmd.Parameters.AddWithValue("@MaterialId", request.MaterialId);
                cmd.Parameters.AddWithValue("@UserId", request.UserId);
                cmd.Parameters.AddWithValue("@Quantity", request.Quantity);
                cmd.Parameters.AddWithValue("@Status", request.Status);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(MaterialRequest request)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    UPDATE Material_Requests
                    SET material_id = @MaterialId,
                        user_id = @UserId,
                        quantity = @Quantity,
                        status = @Status
                    WHERE request_id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", request.RequestId);
                cmd.Parameters.AddWithValue("@MaterialId", request.MaterialId);
                cmd.Parameters.AddWithValue("@UserId", request.UserId);
                cmd.Parameters.AddWithValue("@Quantity", request.Quantity);
                cmd.Parameters.AddWithValue("@Status", request.Status);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Material_Requests WHERE request_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
