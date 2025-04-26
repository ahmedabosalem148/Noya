using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class UserRepository
    {
        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Users", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Phone = reader.GetString(reader.GetOrdinal("phone")),
                            Address = reader.GetString(reader.GetOrdinal("address")),
                            Gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? null : reader.GetString(reader.GetOrdinal("gender")),
                            BirthDate = reader.IsDBNull(reader.GetOrdinal("birth_date")) ? null : reader.GetDateTime(reader.GetOrdinal("birth_date")),
                            ImageUrl = reader.IsDBNull(reader.GetOrdinal("image_url")) ? null : reader.GetString(reader.GetOrdinal("image_url"))
                        });
                    }
                }
            }
            return users;
        }

        public User GetById(int id)
        {
            User user = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Users WHERE user_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Phone = reader.GetString(reader.GetOrdinal("phone")),
                            Address = reader.GetString(reader.GetOrdinal("address")),
                            Gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? null : reader.GetString(reader.GetOrdinal("gender")),
                            BirthDate = reader.IsDBNull(reader.GetOrdinal("birth_date")) ? null : reader.GetDateTime(reader.GetOrdinal("birth_date")),
                            ImageUrl = reader.IsDBNull(reader.GetOrdinal("image_url")) ? null : reader.GetString(reader.GetOrdinal("image_url"))
                        };
                    }
                }
            }
            return user;
        }

        public void Add(User user)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Users (first_name, last_name, email, phone, address, gender, birth_date, image_url) 
                                           VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @Gender, @BirthDate, @ImageUrl)", conn);

                cmd.Parameters.AddWithValue("@FirstName", user.FirstName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LastName", user.LastName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", user.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Gender", user.Gender ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BirthDate", user.BirthDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ImageUrl", user.ImageUrl ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(User user)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"UPDATE Users SET 
                                            first_name=@FirstName,
                                            last_name=@LastName,
                                            email=@Email,
                                            phone=@Phone,
                                            address=@Address,
                                            gender=@Gender,
                                            birth_date=@BirthDate,
                                            image_url=@ImageUrl
                                           WHERE user_id=@UserId", conn);

                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LastName", user.LastName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", user.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Gender", user.Gender ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BirthDate", user.BirthDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ImageUrl", user.ImageUrl ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Users WHERE user_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
