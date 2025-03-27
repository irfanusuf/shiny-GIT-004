
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Middlewares;



namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentApiController(SqlDbContext dbContext, ITokenService tokenService) : ControllerBase
    {
        // private readonly ILogger<OrderController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly SqlDbContext dbContext = dbContext;
        private readonly ITokenService tokenService = tokenService;
        private readonly RazorpayService razorpayService = new RazorpayService();
       
        [HttpPost("create/paymentIntent")]
        public IActionResult CreatePaymentIntent([FromBody] CreatePaymentIntentModel model)
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
                console.log(ex.Message, "Error while creating payment intent.");
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
