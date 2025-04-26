using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class CategoryRepository : IRepository<Category>
    {
        public IEnumerable<Category> GetAll()
        {
            var categories = new List<Category>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Categories", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryId = reader.GetInt32(reader.GetOrdinal("category_id")),
                            CategoryName = reader.GetString(reader.GetOrdinal("category_name"))
                        });
                    }
                }
            }
            return categories;
        }

        public Category GetById(int id)
        {
            Category category = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Categories WHERE category_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        category = new Category
                        {
                            CategoryId = reader.GetInt32(reader.GetOrdinal("category_id")),
                            CategoryName = reader.GetString(reader.GetOrdinal("category_name"))
                        };
                    }
                }
            }
            return category;
        }

        public void Add(Category category)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("INSERT INTO Categories (category_name) VALUES (@Name)", conn);
                cmd.Parameters.AddWithValue("@Name", category.CategoryName);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Category category)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("UPDATE Categories SET category_name=@Name WHERE category_id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", category.CategoryId);
                cmd.Parameters.AddWithValue("@Name", category.CategoryName);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Categories WHERE category_id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
