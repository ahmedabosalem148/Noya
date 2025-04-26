using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class RoleRepository
    {
        public IEnumerable<Role> GetAll()
        {
            var roles = new List<Role>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Roles", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            RoleId = reader.GetInt32(reader.GetOrdinal("role_id")),
                            RoleName = reader.GetString(reader.GetOrdinal("role_name"))
                        });
                    }
                }
            }
            return roles;
        }

        public Role GetById(int id)
        {
            Role role = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Roles WHERE role_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        role = new Role
                        {
                            RoleId = reader.GetInt32(reader.GetOrdinal("role_id")),
                            RoleName = reader.GetString(reader.GetOrdinal("role_name"))
                        };
                    }
                }
            }
            return role;
        }

        public void Add(Role role)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("INSERT INTO Roles (role_name) VALUES (@RoleName)", conn);
                cmd.Parameters.AddWithValue("@RoleName", role.RoleName);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Role role)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("UPDATE Roles SET role_name=@RoleName WHERE role_id=@RoleId", conn);
                cmd.Parameters.AddWithValue("@RoleId", role.RoleId);
                cmd.Parameters.AddWithValue("@RoleName", role.RoleName);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Roles WHERE role_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
