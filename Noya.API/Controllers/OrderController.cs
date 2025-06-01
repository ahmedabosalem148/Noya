using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController()
        {
            _orderService = new OrderService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            var orders = _orderService.GetAll();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("customer/{customerId}")]
        public ActionResult<IEnumerable<Order>> GetByCustomerId(int customerId)
        {
            try
            {
                var orders = _orderService.GetOrdersByCustomerId(customerId);
                if (!orders.Any())
                    return NotFound($"No orders found for customer ID: {customerId}");
                    
                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the orders.");
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] Order order)
        {
            _orderService.Create(order);
            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Order order)
        {
            if (id != order.OrderId) return BadRequest("ID mismatch.");
            _orderService.Update(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return NoContent();
        }

        [HttpPost("checkout")]
        public ActionResult<Order> Checkout([FromBody] CheckoutRequest checkout)
        {
            try
            {
                if (checkout == null)
                    return BadRequest("Checkout request cannot be null");

                var order = _orderService.ProcessCheckout(checkout);
                return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { error = "An error occurred while processing your order. Please try again later." });
            }
        }
    }
}
