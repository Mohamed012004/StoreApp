namespace Store.Route.Domains.Exceptions.BadRequest
{
    public class RegistrationBadRequestException(List<string> errors) : BadRequstException(string.Join(",", errors))
    {
    }
}
