using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class ProductRepository : IRepository<Product>
    {
        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Products", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                            ProductName = reader.GetString(reader.GetOrdinal("product_name")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            Stock = reader.GetInt32(reader.GetOrdinal("stock")),
                            CategoryId = reader.IsDBNull(reader.GetOrdinal("category_id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("category_id")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url"))
                        });
                    }
                }
            }
            return products;
        }

        public Product GetById(int id)
        {
            Product product = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Products WHERE product_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                            ProductName = reader.GetString(reader.GetOrdinal("product_name")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            Stock = reader.GetInt32(reader.GetOrdinal("stock")),
                            CategoryId = reader.IsDBNull(reader.GetOrdinal("category_id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("category_id")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url"))
                        };
                    }
                }
            }
            return product;
        }

        public void Add(Product product)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Products (product_name, description, price, stock, category_id, image_url)
                                           VALUES (@ProductName, @Description, @Price, @Stock, @CategoryId, @ImageUrl)", conn);

                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);
                cmd.Parameters.AddWithValue("@CategoryId", (object)product.CategoryId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ImageUrl", product.ImageUrl);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product product)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"UPDATE Products 
                                           SET product_name=@ProductName, description=@Description, price=@Price, stock=@Stock, category_id=@CategoryId, image_url=@ImageUrl
                                           WHERE product_id=@ProductId", conn);

                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);
                cmd.Parameters.AddWithValue("@CategoryId", (object)product.CategoryId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ImageUrl", product.ImageUrl);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Products WHERE product_id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
