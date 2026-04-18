using Microsoft.AspNetCore.Mvc;
using Store.Route.Services.Abstractions;

namespace Store.Route.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            var result = await _serviceManager.PaymentService.CreatePaymentIntentAsync(basketId);
            return Ok(result);
        }

    }
}
