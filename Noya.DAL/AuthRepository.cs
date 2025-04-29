using System.Data.SqlClient;
using Noya.Models;
using Noya.Models.Noya.Models;

namespace Noya.DAL
{
    public class AuthRepository
    {
        public LoginResponse Login(string email, string password)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
    SELECT 
        u.user_id, 
        u.first_name, 
        u.last_name, 
        u.email,
        r.role_name
    FROM Users u
    INNER JOIN User_Roles ur ON u.user_id = ur.user_id
    INNER JOIN Roles r ON ur.role_id = r.role_id
    WHERE 
        LOWER(LTRIM(RTRIM(u.email))) = LOWER(LTRIM(RTRIM(@Email)))
        AND LTRIM(RTRIM(u.password_hash)) = LTRIM(RTRIM(@Password))", conn);


                cmd.Parameters.AddWithValue("@Email", email.Trim());
                cmd.Parameters.AddWithValue("@Password", password.Trim());

                Console.WriteLine("Email received: " + email);
                Console.WriteLine("Password received: " + password);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new LoginResponse
                        {
                            UserId = reader.GetInt32(0),
                            Name = $"{reader.GetString(1)} {reader.GetString(2)}",
                            Email = reader.GetString(3),
                            Role = reader.GetString(4)
                        };
                    }
                }
            }

            return null;
        }
    }
}
