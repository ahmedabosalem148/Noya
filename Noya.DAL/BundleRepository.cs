using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class BundleRepository
    {
        public IEnumerable<Bundle> GetAll()
        {
            var bundles = new List<Bundle>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Bundles", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bundles.Add(new Bundle
                        {
                            BundleId = reader.GetInt32(reader.GetOrdinal("bundle_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url")),
                            BundleItems = GetBundleItemsByBundleId(reader.GetInt32(reader.GetOrdinal("bundle_id")))
                        });
                    }
                }
            }
            return bundles;
        }

        public Bundle GetById(int id)
        {
            Bundle bundle = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Bundles WHERE bundle_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bundle = new Bundle
                        {
                            BundleId = reader.GetInt32(reader.GetOrdinal("bundle_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url")),
                            BundleItems = GetBundleItemsByBundleId(id)
                        };
                    }
                }
            }
            return bundle;
        }

        public void Add(Bundle bundle)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Bundles (name, price, image_url)
                                           VALUES (@Name, @Price, @ImageUrl);
                                           SELECT SCOPE_IDENTITY();", conn);

                cmd.Parameters.AddWithValue("@Name", bundle.Name);
                cmd.Parameters.AddWithValue("@Price", bundle.Price);
                cmd.Parameters.AddWithValue("@ImageUrl", bundle.ImageUrl);

                bundle.BundleId = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var item in bundle.BundleItems)
                {
                    AddBundleItem(bundle.BundleId, item);
                }
            }
        }

        public void Update(Bundle bundle)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"UPDATE Bundles 
                                           SET name=@Name, price=@Price, image_url=@ImageUrl
                                           WHERE bundle_id=@BundleId", conn);

                cmd.Parameters.AddWithValue("@BundleId", bundle.BundleId);
                cmd.Parameters.AddWithValue("@Name", bundle.Name);
                cmd.Parameters.AddWithValue("@Price", bundle.Price);
                cmd.Parameters.AddWithValue("@ImageUrl", bundle.ImageUrl);

                cmd.ExecuteNonQuery();

                var deleteItemsCmd = new SqlCommand("DELETE FROM Bundle_Items WHERE bundle_id = @BundleId", conn);
                deleteItemsCmd.Parameters.AddWithValue("@BundleId", bundle.BundleId);
                deleteItemsCmd.ExecuteNonQuery();

                foreach (var item in bundle.BundleItems)
                {
                    AddBundleItem(bundle.BundleId, item);
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var deleteItemsCmd = new SqlCommand("DELETE FROM Bundle_Items WHERE bundle_id = @BundleId", conn);
                deleteItemsCmd.Parameters.AddWithValue("@BundleId", id);
                deleteItemsCmd.ExecuteNonQuery();

                var deleteBundleCmd = new SqlCommand("DELETE FROM Bundles WHERE bundle_id = @Id", conn);
                deleteBundleCmd.Parameters.AddWithValue("@Id", id);
                deleteBundleCmd.ExecuteNonQuery();
            }
        }

        private List<BundleItem> GetBundleItemsByBundleId(int bundleId)
        {
            var items = new List<BundleItem>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Bundle_Items WHERE bundle_id = @BundleId", conn);
                cmd.Parameters.AddWithValue("@BundleId", bundleId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new BundleItem
                        {
                            BundleId = reader.GetInt32(reader.GetOrdinal("bundle_id")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity"))
                        });
                    }
                }
            }
            return items;
        }

        private void AddBundleItem(int bundleId, BundleItem item)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Bundle_Items (bundle_id, product_id, quantity)
                                           VALUES (@BundleId, @ProductId, @Quantity)", conn);

                cmd.Parameters.AddWithValue("@BundleId", bundleId);
                cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
