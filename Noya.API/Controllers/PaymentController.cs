using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController()
        {
            _paymentService = new PaymentService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Payment>> GetAll()
        {
            var payments = _paymentService.GetAll();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public ActionResult<Payment> GetById(int id)
        {
            var payment = _paymentService.GetById(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Payment payment)
        {
            _paymentService.Create(payment);
            return CreatedAtAction(nameof(GetById), new { id = payment.PaymentId }, payment);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Payment payment)
        {
            if (id != payment.PaymentId) return BadRequest("ID mismatch.");
            _paymentService.Update(payment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _paymentService.Delete(id);
            return NoContent();
        }
    }
}
