using System.Data.SqlClient;

namespace Noya.DAL
{
    public static class DbHelper
    {
        private static readonly string _connectionString = "Server=localhost;Database=NoyaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}

