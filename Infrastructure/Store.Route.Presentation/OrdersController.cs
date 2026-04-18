using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Route.Services.Abstractions;
using Store.Route.Shared.DTOs.Orders;
using System.Security.Claims;

namespace Store.Route.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager _sevceManager) : ControllerBase
    {
        [HttpPost] // POST: BaseUrl/api/orders
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            var userEmailCliam = User.FindFirst(ClaimTypes.Email);
            var result = await _sevceManager.OrderService.CreateOrderAsync(request, userEmailCliam.Value);

            return Ok(result);
        }

        // get All Delivery Methods
        [HttpGet("DeliveryMethods")] // POST: BaseUrl/api/orders/deliveryMethod
        public async Task<IActionResult> GetAllDeliveryMethod()
        {
            var result = await _sevceManager.OrderService.GetAllDeliveryMethodsAsync();
            return Ok(result);
        }

        // get  Order by id For User
        [HttpGet("{id}")] // POST: BaseUrl/api/orders/id
        [Authorize]
        public async Task<IActionResult> GetOrderByIdForSpecificUser(Guid id)
        {
            var useEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _sevceManager.OrderService.GetOrderByIdForSpecificUserAsync(id, useEmailClaim.Value);
            return Ok(result);
        }

        // get  Orders For User
        [HttpGet] // POST: BaseUrl/api/orders
        [Authorize]
        public async Task<IActionResult> GetOrdersForSpecificUser()
        {
            var useEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _sevceManager.OrderService.GetOrdersForSpecificUserAsync(useEmailClaim.Value);
            return Ok(result);
        }
    }
}
