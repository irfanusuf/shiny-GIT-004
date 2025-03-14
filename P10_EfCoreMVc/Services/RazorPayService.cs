using Razorpay.Api;


public class RazorpayService
{
    private readonly string key = "rzp_test_RgG9AwfnysizZu";
    private readonly string secret = "1CaqfrrZdfeKKOIdPypjADfr";

    public Order? CreateOrder(int amount, string currency , Guid orderId)
    {
        try
        {
            RazorpayClient client = new(key, secret);

            Dictionary<string, object> options = new Dictionary<string, object>
            {
                { "amount", amount * 100 }, 
                { "currency", currency },
                { "receipt", orderId },
                { "payment_capture", 1 } 
            };

            Order order = client.Order.Create(options);
            return order;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
