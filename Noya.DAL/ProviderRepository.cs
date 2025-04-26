using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class ProviderRepository : IRepository<Provider>
    {
        public IEnumerable<Provider> GetAll()
        {
            var providers = new List<Provider>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Providers", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        providers.Add(new Provider
                        {
                            ProviderId = reader.GetInt32(reader.GetOrdinal("provider_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            ContactEmail = reader.GetString(reader.GetOrdinal("contact_email")),
                            Phone = reader.GetString(reader.GetOrdinal("phone")),
                            Location = reader.GetString(reader.GetOrdinal("location"))
                        });
                    }
                }
            }
            return providers;
        }

        public Provider GetById(int id)
        {
            Provider provider = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Providers WHERE provider_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        provider = new Provider
                        {
                            ProviderId = reader.GetInt32(reader.GetOrdinal("provider_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            ContactEmail = reader.GetString(reader.GetOrdinal("contact_email")),
                            Phone = reader.GetString(reader.GetOrdinal("phone")),
                            Location = reader.GetString(reader.GetOrdinal("location"))
                        };
                    }
                }
            }
            return provider;
        }

        public void Add(Provider provider)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Providers (name, contact_email, phone, location)
                                           VALUES (@Name, @ContactEmail, @Phone, @Location)", conn);

                cmd.Parameters.AddWithValue("@Name", provider.Name);
                cmd.Parameters.AddWithValue("@ContactEmail", provider.ContactEmail);
                cmd.Parameters.AddWithValue("@Phone", provider.Phone);
                cmd.Parameters.AddWithValue("@Location", provider.Location);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Provider provider)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"UPDATE Providers 
                                           SET name=@Name, contact_email=@ContactEmail, phone=@Phone, location=@Location
                                           WHERE provider_id=@ProviderId", conn);

                cmd.Parameters.AddWithValue("@ProviderId", provider.ProviderId);
                cmd.Parameters.AddWithValue("@Name", provider.Name);
                cmd.Parameters.AddWithValue("@ContactEmail", provider.ContactEmail);
                cmd.Parameters.AddWithValue("@Phone", provider.Phone);
                cmd.Parameters.AddWithValue("@Location", provider.Location);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Providers WHERE provider_id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
