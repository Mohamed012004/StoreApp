namespace Store.Route.Domains.Exceptions.ValidationError
{
    public class ValidationError
    {
        public string Field { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
