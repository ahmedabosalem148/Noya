using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class ShippingAgencyRepository : IRepository<ShippingAgency>
    {
        public IEnumerable<ShippingAgency> GetAll()
        {
            var agencies = new List<ShippingAgency>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Shipping_Agency", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        agencies.Add(new ShippingAgency
                        {
                            AgencyId = reader.GetInt32(reader.GetOrdinal("agency_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Phone = reader.GetString(reader.GetOrdinal("phone")),
                            Address = reader.GetString(reader.GetOrdinal("address"))
                        });
                    }
                }
            }
            return agencies;
        }

        public ShippingAgency GetById(int id)
        {
            ShippingAgency agency = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Shipping_Agency WHERE agency_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        agency = new ShippingAgency
                        {
                            AgencyId = reader.GetInt32(reader.GetOrdinal("agency_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Phone = reader.GetString(reader.GetOrdinal("phone")),
                            Address = reader.GetString(reader.GetOrdinal("address"))
                        };
                    }
                }
            }
            return agency;
        }

        public void Add(ShippingAgency agency)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Shipping_Agency (name, phone, address)
                                           VALUES (@Name, @Phone, @Address)", conn);

                cmd.Parameters.AddWithValue("@Name", agency.Name);
                cmd.Parameters.AddWithValue("@Phone", agency.Phone);
                cmd.Parameters.AddWithValue("@Address", agency.Address);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(ShippingAgency agency)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"UPDATE Shipping_Agency 
                                           SET name=@Name, phone=@Phone, address=@Address
                                           WHERE agency_id=@AgencyId", conn);

                cmd.Parameters.AddWithValue("@AgencyId", agency.AgencyId);
                cmd.Parameters.AddWithValue("@Name", agency.Name);
                cmd.Parameters.AddWithValue("@Phone", agency.Phone);
                cmd.Parameters.AddWithValue("@Address", agency.Address);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Shipping_Agency WHERE agency_id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
