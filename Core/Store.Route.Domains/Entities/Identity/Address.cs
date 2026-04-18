namespace Store.Route.Domains.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // Navigation Pproperty
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }  // F.K
    }
}
