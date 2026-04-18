using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities.Order;
using Store.Route.Domains.Exceptions.NotFound;
using Store.Route.Services.Abstractions.Payments;
using Store.Route.Shared.DTOs.Baskets;
using Stripe;

namespace Store.Route.Services.Payments
{
    public class PaymentService(IBasketRepository _basketRepository, IUnitOfWork _unitOfWork, IConfiguration configuration, IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreatePaymentIntentAsync(string basketId)
        {
            // Calculate Amount = subTotal + Delivery Method Cost


            // get basket By id
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) throw new BasketNotFoundException(basketId);

            // check product and its price 
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }
            // calculate SubTotal  
            var subtotal = basket.Items.Sum(I => I.Price * I.Quantity);

            // get Delivery Method By Id
            if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFoundException(-1);

            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            basket.ShippingPrice = deliveryMethod.Price;

            var amount = subtotal + deliveryMethod.Price;

            // Sent  To Amount Stripe
            StripeConfiguration.ApiKey = configuration["StripeOptions:SecretKey"];
            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (basket.PaymentIntentId is null)
            {
                // Create
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)amount * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);
            }
            else
            {
                // Update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)amount * 100,
                };
                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }
            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;

            basket = await _basketRepository.CreateBasketAsync(basket, TimeSpan.FromDays(1));
            return _mapper.Map<BasketDto>(basket);
        }
    }
}
