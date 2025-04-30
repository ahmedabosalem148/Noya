using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class MaterialRepository
    {
        public IEnumerable<Material> GetAll()
        {
            var materials = new List<Material>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Materials", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        materials.Add(new Material
                        {
                            MaterialId = reader.GetInt32(reader.GetOrdinal("material_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                            StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity")),
                            ProviderId = reader.IsDBNull(reader.GetOrdinal("provider_id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("provider_id")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url"))
                        });
                    }
                }
            }
            return materials;
        }

        public Material GetById(int id)
        {
            Material material = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Materials WHERE material_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        material = new Material
                        {
                            MaterialId = reader.GetInt32(reader.GetOrdinal("material_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                            StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity")),
                            ProviderId = reader.IsDBNull(reader.GetOrdinal("provider_id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("provider_id")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url"))
                        };
                    }
                }
            }
            return material;
        }

        public void Add(Material material)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    INSERT INTO Materials (name, description, unit_price, stock_quantity, provider_id, image_url)
                    VALUES (@Name, @Description, @UnitPrice, @StockQuantity, @ProviderId, @ImageUrl)", conn);

                cmd.Parameters.AddWithValue("@Name", material.Name);
                cmd.Parameters.AddWithValue("@Description", material.Description);
                cmd.Parameters.AddWithValue("@UnitPrice", material.UnitPrice);
                cmd.Parameters.AddWithValue("@StockQuantity", material.StockQuantity);
                cmd.Parameters.AddWithValue("@ProviderId", (object)material.ProviderId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ImageUrl", material.ImageUrl);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Material material)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    UPDATE Materials 
                    SET name = @Name,
                        description = @Description,
                        unit_price = @UnitPrice,
                        stock_quantity = @StockQuantity,
                        provider_id = @ProviderId,
                        image_url = @ImageUrl
                    WHERE material_id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", material.MaterialId);
                cmd.Parameters.AddWithValue("@Name", material.Name);
                cmd.Parameters.AddWithValue("@Description", material.Description);
                cmd.Parameters.AddWithValue("@UnitPrice", material.UnitPrice);
                cmd.Parameters.AddWithValue("@StockQuantity", material.StockQuantity);
                cmd.Parameters.AddWithValue("@ProviderId", (object)material.ProviderId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ImageUrl", material.ImageUrl);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Materials WHERE material_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
