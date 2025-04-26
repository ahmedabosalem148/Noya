using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class PaymentRepository : IRepository<Payment>
    {
        public IEnumerable<Payment> GetAll()
        {
            var payments = new List<Payment>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Payment", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payments.Add(new Payment
                        {
                            PaymentId = reader.GetInt32(reader.GetOrdinal("payment_id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            PaymentMethod = reader.GetString(reader.GetOrdinal("payment_method")),
                            TransactionAmount = reader.GetDecimal(reader.GetOrdinal("transaction_amount")),
                            TransactionStatus = reader.GetString(reader.GetOrdinal("transaction_status")),
                            TransactionDate = reader.GetDateTime(reader.GetOrdinal("transaction_date"))
                        });
                    }
                }
            }
            return payments;
        }

        public Payment GetById(int id)
        {
            Payment payment = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Payment WHERE payment_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        payment = new Payment
                        {
                            PaymentId = reader.GetInt32(reader.GetOrdinal("payment_id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            PaymentMethod = reader.GetString(reader.GetOrdinal("payment_method")),
                            TransactionAmount = reader.GetDecimal(reader.GetOrdinal("transaction_amount")),
                            TransactionStatus = reader.GetString(reader.GetOrdinal("transaction_status")),
                            TransactionDate = reader.GetDateTime(reader.GetOrdinal("transaction_date"))
                        };
                    }
                }
            }
            return payment;
        }

        public void Add(Payment payment)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Payment (user_id, payment_method, transaction_amount, transaction_status)
                                           VALUES (@UserId, @PaymentMethod, @TransactionAmount, @TransactionStatus)", conn);

                cmd.Parameters.AddWithValue("@UserId", payment.UserId);
                cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                cmd.Parameters.AddWithValue("@TransactionAmount", payment.TransactionAmount);
                cmd.Parameters.AddWithValue("@TransactionStatus", payment.TransactionStatus);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Payment payment)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"UPDATE Payment 
                                           SET user_id=@UserId, payment_method=@PaymentMethod, transaction_amount=@TransactionAmount, transaction_status=@TransactionStatus
                                           WHERE payment_id=@PaymentId", conn);

                cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId);
                cmd.Parameters.AddWithValue("@UserId", payment.UserId);
                cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                cmd.Parameters.AddWithValue("@TransactionAmount", payment.TransactionAmount);
                cmd.Parameters.AddWithValue("@TransactionStatus", payment.TransactionStatus);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Payment WHERE payment_id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
