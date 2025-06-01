using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;
using System;
using System.Linq;
using System.Data.SqlClient;

namespace Noya.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductRepository _productRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
            _productRepository = new ProductRepository();
        }

        public IEnumerable<Order> GetAll() => _orderRepository.GetAll();

        public Order GetById(int id) => _orderRepository.GetById(id);

        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer ID must be greater than zero.", nameof(customerId));

            return _orderRepository.GetOrdersByCustomerId(customerId);
        }

        public void Create(Order order)
        {
            ValidateOrder(order);
            _orderRepository.AddOrderWithProducts(order);
        }

        private void ValidateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null");

            if (order.UserId <= 0)
                throw new ArgumentException("User ID must be greater than zero.", nameof(order.UserId));

            if (string.IsNullOrWhiteSpace(order.ShippingAddress))
                throw new ArgumentException("Shipping address is required.", nameof(order.ShippingAddress));

            if (order.OrderItems == null || !order.OrderItems.Any())
                throw new ArgumentException("Order must contain at least one item.", nameof(order.OrderItems));

            // Validate each order item
            foreach (var item in order.OrderItems)
            {
                if (item.ProductId <= 0)
                    throw new ArgumentException($"Invalid product ID for item {item.OrderItemId}");

                if (item.Quantity <= 0)
                    throw new ArgumentException($"Quantity must be greater than zero for product {item.ProductId}");

                // Check if product exists and has enough stock
                var product = _productRepository.GetById(item.ProductId);
                if (product == null)
                    throw new ArgumentException($"Product with ID {item.ProductId} does not exist");

                if (product.Stock < item.Quantity)
                    throw new ArgumentException($"Not enough stock for product {product.ProductName}. Available: {product.Stock}, Requested: {item.Quantity}");
            }

            // Calculate total price
            decimal totalPrice = order.OrderItems.Sum(item => item.Price * item.Quantity);
            if (totalPrice != order.TotalPrice)
                throw new ArgumentException($"Total price mismatch. Calculated: {totalPrice}, Provided: {order.TotalPrice}");
        }

        public void Update(Order order) => _orderRepository.Update(order);

        public void Delete(int id) => _orderRepository.Delete(id);

        public Order ProcessCheckout(CheckoutRequest checkout)
        {
            try
            {
                if (checkout == null)
                    throw new ArgumentNullException(nameof(checkout), "Checkout request cannot be null");

                if (checkout.UserId <= 0)
                    throw new ArgumentException("Invalid user ID", nameof(checkout.UserId));

                if (string.IsNullOrWhiteSpace(checkout.ShippingAddress))
                    throw new ArgumentException("Shipping address is required", nameof(checkout.ShippingAddress));

                if (checkout.Items == null || !checkout.Items.Any())
                    throw new ArgumentException("Order must contain at least one item", nameof(checkout.Items));

                // Get current prices and validate products
                var productPrices = new Dictionary<int, decimal>();
                var productNames = new Dictionary<int, string>();

                foreach (var item in checkout.Items)
                {
                    var product = _productRepository.GetById(item.ProductId);
                    if (product == null)
                        throw new ArgumentException($"Product with ID {item.ProductId} does not exist");

                    if (item.Quantity <= 0)
                        throw new ArgumentException($"Invalid quantity for product {product.ProductName}");

                    if (product.Stock < item.Quantity)
                        throw new ArgumentException($"Not enough stock for product {product.ProductName}. Available: {product.Stock}, Requested: {item.Quantity}");

                    productPrices[item.ProductId] = product.Price;
                    productNames[item.ProductId] = product.ProductName;
                }

                // Validate total price calculation
                decimal totalPrice = checkout.Items.Sum(item => productPrices[item.ProductId] * item.Quantity);
                if (totalPrice <= 0)
                    throw new ArgumentException("Total price must be greater than zero");

                return _orderRepository.ProcessCheckout(checkout, productPrices);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error occurred while processing checkout", ex);
            }
            catch (Exception ex) when (!(ex is ArgumentException))
            {
                throw new Exception("An error occurred while processing checkout", ex);
            }
        }
    }
}
