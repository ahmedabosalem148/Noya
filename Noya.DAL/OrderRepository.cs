using System.Collections.Generic;
using System.Data.SqlClient;
using Noya.Models;

namespace Noya.DAL
{
    public class OrderRepository
    {
        public IEnumerable<Order> GetAll()
        {
            var orders = new List<Order>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Orders", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new Order
                        {
                            OrderId = reader.GetInt32(reader.GetOrdinal("order_id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("order_date")),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal("total_price")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                            ShippingAddress = reader.GetString(reader.GetOrdinal("shipping_address")),
                            OrderItems = GetOrderItemsByOrderId(reader.GetInt32(reader.GetOrdinal("order_id")))
                        };
                        orders.Add(order);
                    }
                }
            }
            return orders;
        }

        public Order GetById(int id)
        {
            Order order = null;
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Orders WHERE order_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order = new Order
                        {
                            OrderId = reader.GetInt32(reader.GetOrdinal("order_id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("order_date")),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal("total_price")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                            ShippingAddress = reader.GetString(reader.GetOrdinal("shipping_address")),
                            OrderItems = GetOrderItemsByOrderId(id)
                        };
                    }
                }
            }
            return order;
        }

        public void Add(Order order)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Orders (user_id, order_date, total_price, status, shipping_address)
                                           VALUES (@UserId, @OrderDate, @TotalPrice, @Status, @ShippingAddress);
                                           SELECT SCOPE_IDENTITY();", conn);

                cmd.Parameters.AddWithValue("@UserId", order.UserId);
                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                cmd.Parameters.AddWithValue("@Status", order.Status);
                cmd.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);

                order.OrderId = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var item in order.OrderItems)
                {
                    AddOrderItem(order.OrderId, item);
                }
            }
        }

        public void Update(Order order)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"UPDATE Orders 
                                           SET user_id=@UserId, order_date=@OrderDate, total_price=@TotalPrice, 
                                               status=@Status, shipping_address=@ShippingAddress
                                           WHERE order_id=@OrderId", conn);

                cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                cmd.Parameters.AddWithValue("@UserId", order.UserId);
                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                cmd.Parameters.AddWithValue("@Status", order.Status);
                cmd.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Orders WHERE order_id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            var items = new List<OrderItem>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Order_Items WHERE order_id = @OrderId", conn);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new OrderItem
                        {
                            OrderItemId = reader.GetInt32(reader.GetOrdinal("order_item_id")),
                            OrderId = reader.GetInt32(reader.GetOrdinal("order_id")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price"))
                        });
                    }
                }
            }
            return items;
        }

        private void AddOrderItem(int orderId, OrderItem item)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"INSERT INTO Order_Items (order_id, product_id, quantity, price)
                                           VALUES (@OrderId, @ProductId, @Quantity, @Price)", conn);

                cmd.Parameters.AddWithValue("@OrderId", orderId);
                cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@Price", item.Price);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
