using Microsoft.AspNetCore.Mvc;
using Store.Route.Services.Abstractions;
using Store.Route.Shared.DTOs.Baskets;

namespace Store.Route.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet] // GET: baseUrl/api/baskets?id
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await _serviceManager.BasketService.GetBasketasync(id);

            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket([FromBody] BasketDto basketDto)
        {
            if (basketDto == null)
            {
                return BadRequest("Basket data is required.");
            }

            // لو الـ Id مش موجود، تولد واحد جديد
            if (string.IsNullOrEmpty(basketDto.Id))
            {
                basketDto.Id = Guid.NewGuid().ToString();
            }

            // تحقق من الـ items
            if (basketDto.Items == null || !basketDto.Items.Any())
            {
                return BadRequest("Basket must have at least one item.");
            }

            foreach (var item in basketDto.Items)
            {
                if (item.Id <= 0)
                {
                    return BadRequest($"Invalid product Id {item.Id} in items.");
                }
            }

            try
            {
                var result = await _serviceManager.BasketService.CreateBasketasync(basketDto, TimeSpan.FromDays(1));
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Basket POST error: " + ex.Message);
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasketById(string id)
        {
            var result = await _serviceManager.BasketService.DeleteBasketasync(id);
            return NoContent();
        }

    }
}
