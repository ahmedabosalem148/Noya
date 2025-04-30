using Microsoft.AspNetCore.Mvc;
using Noya.BLL;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinishingRequestController : ControllerBase
    {
        private readonly ReportService _service = new ReportService();

        public class FinishingRequestDto
        {
            public int UserId { get; set; }        // مؤقتًا، لحين دمجه مع Firebase auth
            public int Area { get; set; }
            public decimal Budget { get; set; }
            public string Address { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] FinishingRequestDto request)
        {
            _service.CreateFinishingRequest(request.UserId, request.Area, request.Budget, request.Address);
            return Ok(new { message = "✅ تم إرسال طلب التشطيب بنجاح" });
        }
    }
}
