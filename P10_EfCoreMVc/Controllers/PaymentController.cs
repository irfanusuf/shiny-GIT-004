
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Data;
using WebApplication1.Interfaces;



namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(SqlDbContext dbContext, ITokenService tokenService, ILogger<OrderController> logger) : ControllerBase
    {
        private readonly ILogger<OrderController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly SqlDbContext dbContext = dbContext;
        private readonly ITokenService tokenService = tokenService;
        private readonly RazorpayService razorpayService = new RazorpayService();
       
        [HttpPost("create-intent")]
        public IActionResult CreateIntent([FromBody] CreatePaymentIntentModel model)
        {
            if (model == null || model.Amount <= 0 || string.IsNullOrEmpty(model.Currency))
            {
                return BadRequest("Invalid payment details.");
            }
            try
            {
                var order = razorpayService.CreateOrder(model.Amount, model.Currency, model.OrderId);

                if (order == null)
                {
                    return StatusCode(500, "Failed to create payment order.");
                }

                return Ok(new
                {
                    orderId = order["id"].ToString(),
                    entity = order["entity"].ToString(),
                    amount = order["amount"],
                    amountPaid = order["amount_paid"],
                    amountDue = order["amount_due"],
                    currency = order["currency"].ToString(),
                    receipt = order["receipt"].ToString(),
                    status = order["status"].ToString(),
                    attempts = order["attempts"],
                    createdAt = order["created_at"]
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating payment intent.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        public class CreatePaymentIntentModel
        {
            public int Amount { get; set; }
            public string? Currency { get; set; }
            public Guid OrderId {get ; set;}
        }
    }
}
