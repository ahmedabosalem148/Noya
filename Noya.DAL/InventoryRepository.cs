using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class InventoryRepository : IRepository<Inventory>
    {
        public IEnumerable<Inventory> GetAll()
        {
            var inventories = new List<Inventory>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Inventory", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inventories.Add(new Inventory
                        {
                            InventoryId = reader.GetInt32(reader.GetOrdinal("inventory_id")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                            ProviderId = reader.GetInt32(reader.GetOrdinal("provider_id")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            LastUpdated = reader.GetDateTime(reader.GetOrdinal("last_updated"))
                        });
                    }
                }
            }
            return inventories;
        }

        public Inventory GetById(int id)
        {
            Inventory inventory = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Inventory WHERE inventory_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inventory = new Inventory
                        {
                            InventoryId = reader.GetInt32(reader.GetOrdinal("inventory_id")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                            ProviderId = reader.GetInt32(reader.GetOrdinal("provider_id")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            LastUpdated = reader.GetDateTime(reader.GetOrdinal("last_updated"))
                        };
                    }
                }
            }
            return inventory;
        }

        public void Add(Inventory inventory)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Inventory (product_id, provider_id, quantity)
                                           VALUES (@ProductId, @ProviderId, @Quantity)", conn);

                cmd.Parameters.AddWithValue("@ProductId", inventory.ProductId);
                cmd.Parameters.AddWithValue("@ProviderId", inventory.ProviderId);
                cmd.Parameters.AddWithValue("@Quantity", inventory.Quantity);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Inventory inventory)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"UPDATE Inventory 
                                           SET product_id=@ProductId, provider_id=@ProviderId, quantity=@Quantity
                                           WHERE inventory_id=@InventoryId", conn);

                cmd.Parameters.AddWithValue("@InventoryId", inventory.InventoryId);
                cmd.Parameters.AddWithValue("@ProductId", inventory.ProductId);
                cmd.Parameters.AddWithValue("@ProviderId", inventory.ProviderId);
                cmd.Parameters.AddWithValue("@Quantity", inventory.Quantity);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Inventory WHERE inventory_id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
