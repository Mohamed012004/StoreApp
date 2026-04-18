using AutoMapper;
using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities.Order;
using Store.Route.Domains.Exceptions.BadRequest;
using Store.Route.Domains.Exceptions.NotFound;
using Store.Route.Services.Abstractions.Order;
using Store.Route.Services.Specifications;
using Store.Route.Shared.DTOs.Orders;

namespace Store.Route.Services.Orders
{
    public class OrderService(IUnitOfWork _unitOfWork, IBasketRepository _basketRepository, IMapper _mapper) : IOrderSevice
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            // 1 Get Order Address
            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);
            // 2. Get Delivery Method By DeliveryMethodId
            var deliveyMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (deliveyMethod is null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

            //3. Get Order Item
            // 3.1 Get Basket By Id
            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);

            // 3.2 Convert Evey Basket Item To Order Item
            var ordertems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                //  Check Price
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                if (item.Price != product.Price) item.Price = product.Price;

                // Get Order Item
                var productInOrder = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrder, item.Quantity, item.Price);

                // Add To List
                ordertems.Add(orderItem);
            }

            //4. Calculate SunTotal = Price of Item * Quantity
            var subtotal = ordertems.Sum(OI => OI.Price * OI.Quantity);

            //5. PaymentIntent Id
            //var spec = 


            // Create Order
            var order = new Order(userEmail, orderAddress, deliveyMethod, ordertems, subtotal, basket.PaymentIntentId);

            // Add Order in Data Base
            await _unitOfWork.GetRepository<Guid, Order>().AddAsync(order);
            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new CreateOrderBadRequestException();

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<DeliveryMethodsResponse>> GetAllDeliveryMethodsAsync()
        {
            var deliveryMethodId = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAllAsync(false);
            return _mapper.Map<IEnumerable<DeliveryMethodsResponse>>(deliveryMethodId);
        }

        public async Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string UserEmail)
        {
            var spec = new OrderSpecification(id, UserEmail);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);
            if (order is null) throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResponse>(order);

        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string UserEmail)
        {
            var spec = new OrderSpecification(UserEmail);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(spec, false);
            return _mapper.Map<IEnumerable<OrderResponse>>(order);
        }

    }
}
