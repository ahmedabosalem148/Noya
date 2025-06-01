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

        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            var orders = new List<Order>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Orders WHERE user_id = @CustomerId", conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                
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

        public void AddOrderWithProducts(Order order)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // Start transaction
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert the order
                        var cmd = new SqlCommand(@"INSERT INTO Orders (user_id, order_date, total_price, status, shipping_address)
                                               VALUES (@UserId, @OrderDate, @TotalPrice, @Status, @ShippingAddress);
                                               SELECT SCOPE_IDENTITY();", conn, transaction);

                        cmd.Parameters.AddWithValue("@UserId", order.UserId);
                        cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                        cmd.Parameters.AddWithValue("@Status", order.Status);
                        cmd.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);

                        order.OrderId = Convert.ToInt32(cmd.ExecuteScalar());

                        // Insert all order items
                        foreach (var item in order.OrderItems)
                        {
                            var itemCmd = new SqlCommand(@"INSERT INTO Order_Items (order_id, product_id, quantity, price)
                                                       VALUES (@OrderId, @ProductId, @Quantity, @Price)", conn, transaction);

                            itemCmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                            itemCmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                            itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            itemCmd.Parameters.AddWithValue("@Price", item.Price);

                            itemCmd.ExecuteNonQuery();
                        }

                        // Commit transaction
                        transaction.Commit();
                    }
                    catch
                    {
                        // Rollback transaction on error
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            var items = new List<OrderItem>();
            using (var conn = DbHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    SELECT oi.*, 
                           p.product_name, p.description, p.price as product_price, 
                           p.stock, p.image_url, p.category_id,
                           c.category_name
                    FROM Order_Items oi
                    INNER JOIN Products p ON oi.product_id = p.product_id
                    LEFT JOIN Categories c ON p.category_id = c.category_id
                    WHERE oi.order_id = @OrderId", conn);
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
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            Product = new Product
                            {
                                ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                                ProductName = reader.GetString(reader.GetOrdinal("product_name")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Price = reader.GetDecimal(reader.GetOrdinal("product_price")),
                                Stock = reader.GetInt32(reader.GetOrdinal("stock")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("image_url")),
                                CategoryId = reader.IsDBNull(reader.GetOrdinal("category_id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("category_id")),
                                Category = reader.IsDBNull(reader.GetOrdinal("category_id")) ? null : new Category
                                {
                                    CategoryId = reader.GetInt32(reader.GetOrdinal("category_id")),
                                    CategoryName = reader.GetString(reader.GetOrdinal("category_name"))
                                }
                            }
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

        public Order ProcessCheckout(CheckoutRequest checkout, Dictionary<int, decimal> productPrices)
        {
            using (var conn = DbHelper.GetConnection())
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Calculate total price
                        decimal totalPrice = 0;
                        foreach (var item in checkout.Items)
                        {
                            totalPrice += productPrices[item.ProductId] * item.Quantity;
                        }

                        // Create the order
                        var order = new Order
                        {
                            UserId = checkout.UserId,
                            OrderDate = DateTime.Now,
                            TotalPrice = totalPrice,
                            Status = "Pending",
                            ShippingAddress = checkout.ShippingAddress,
                            OrderItems = new List<OrderItem>()
                        };

                        // Insert order
                        var cmd = new SqlCommand(@"
                            INSERT INTO Orders (user_id, order_date, total_price, status, shipping_address)
                            OUTPUT INSERTED.order_id
                            VALUES (@UserId, @OrderDate, @TotalPrice, @Status, @ShippingAddress);", conn, transaction);

                        cmd.Parameters.AddWithValue("@UserId", order.UserId);
                        cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                        cmd.Parameters.AddWithValue("@Status", order.Status);
                        cmd.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);

                        order.OrderId = Convert.ToInt32(cmd.ExecuteScalar());

                        // Insert order items
                        foreach (var item in checkout.Items)
                        {
                            var itemCmd = new SqlCommand(@"
                                INSERT INTO Order_Items (order_id, product_id, quantity, price)
                                OUTPUT INSERTED.order_item_id
                                VALUES (@OrderId, @ProductId, @Quantity, @Price)", conn, transaction);

                            itemCmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                            itemCmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                            itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            itemCmd.Parameters.AddWithValue("@Price", productPrices[item.ProductId]);

                            var orderItemId = Convert.ToInt32(itemCmd.ExecuteScalar());

                            // Add to order items collection
                            order.OrderItems.Add(new OrderItem
                            {
                                OrderItemId = orderItemId,
                                OrderId = order.OrderId,
                                ProductId = item.ProductId,
                                Quantity = item.Quantity,
                                Price = productPrices[item.ProductId]
                            });
                        }

                        // Update product stock
                        foreach (var item in checkout.Items)
                        {
                            var updateStockCmd = new SqlCommand(@"
                                UPDATE Products 
                                SET stock = stock - @Quantity 
                                WHERE product_id = @ProductId", conn, transaction);

                            updateStockCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            updateStockCmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                            updateStockCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        // Get the complete order with all details
                        return GetById(order.OrderId);
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
