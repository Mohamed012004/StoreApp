using Store.Route.Domains.Entities.Order;

namespace Store.Route.Services.Specifications
{
    public class OrderSpecification : BaseSpecifications<Guid, Order>
    {
        public OrderSpecification(Guid id, string userEmail) : base(O => O.Id == id && O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeleveryMethod);
            Includes.Add(O => O.Items);

        }

        public OrderSpecification(string userEmail) : base(O => O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeleveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDescending(O => O.OrderDate);
        }
    }
}
