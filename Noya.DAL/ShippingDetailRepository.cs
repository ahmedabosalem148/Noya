using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class ShippingDetailRepository : IRepository<ShippingDetail>
    {
        public IEnumerable<ShippingDetail> GetAll()
        {
            var shippingDetails = new List<ShippingDetail>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Shipping_Details", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shippingDetails.Add(new ShippingDetail
                        {
                            ShippingId = reader.GetInt32(reader.GetOrdinal("shipping_id")),
                            OrderId = reader.GetInt32(reader.GetOrdinal("order_id")),
                            AgencyId = reader.IsDBNull(reader.GetOrdinal("agency_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("agency_id")),
                            TrackingNumber = reader.GetString(reader.GetOrdinal("tracking_number")),
                            EstimatedDeliveryDate = reader.IsDBNull(reader.GetOrdinal("estimated_delivery_date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("estimated_delivery_date")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                            ShippingAddress = reader.GetString(reader.GetOrdinal("shipping_address"))
                        });
                    }
                }
            }
            return shippingDetails;
        }

        public ShippingDetail GetById(int id)
        {
            ShippingDetail detail = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Shipping_Details WHERE shipping_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        detail = new ShippingDetail
                        {
                            ShippingId = reader.GetInt32(reader.GetOrdinal("shipping_id")),
                            OrderId = reader.GetInt32(reader.GetOrdinal("order_id")),
                            AgencyId = reader.IsDBNull(reader.GetOrdinal("agency_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("agency_id")),
                            TrackingNumber = reader.GetString(reader.GetOrdinal("tracking_number")),
                            EstimatedDeliveryDate = reader.IsDBNull(reader.GetOrdinal("estimated_delivery_date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("estimated_delivery_date")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                            ShippingAddress = reader.GetString(reader.GetOrdinal("shipping_address"))
                        };
                    }
                }
            }
            return detail;
        }

        public void Add(ShippingDetail detail)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Shipping_Details (order_id, agency_id, tracking_number, estimated_delivery_date, status, shipping_address)
                                           VALUES (@OrderId, @AgencyId, @TrackingNumber, @EstimatedDeliveryDate, @Status, @ShippingAddress)", conn);

                cmd.Parameters.AddWithValue("@OrderId", detail.OrderId);
                cmd.Parameters.AddWithValue("@AgencyId", (object)detail.AgencyId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TrackingNumber", detail.TrackingNumber);
                cmd.Parameters.AddWithValue("@EstimatedDeliveryDate", (object)detail.EstimatedDeliveryDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", detail.Status);
                cmd.Parameters.AddWithValue("@ShippingAddress", detail.ShippingAddress);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(ShippingDetail detail)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"UPDATE Shipping_Details 
                                           SET order_id=@OrderId, agency_id=@AgencyId, tracking_number=@TrackingNumber, estimated_delivery_date=@EstimatedDeliveryDate, status=@Status, shipping_address=@ShippingAddress
                                           WHERE shipping_id=@ShippingId", conn);

                cmd.Parameters.AddWithValue("@ShippingId", detail.ShippingId);
                cmd.Parameters.AddWithValue("@OrderId", detail.OrderId);
                cmd.Parameters.AddWithValue("@AgencyId", (object)detail.AgencyId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TrackingNumber", detail.TrackingNumber);
                cmd.Parameters.AddWithValue("@EstimatedDeliveryDate", (object)detail.EstimatedDeliveryDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", detail.Status);
                cmd.Parameters.AddWithValue("@ShippingAddress", detail.ShippingAddress);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Shipping_Details WHERE shipping_id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
