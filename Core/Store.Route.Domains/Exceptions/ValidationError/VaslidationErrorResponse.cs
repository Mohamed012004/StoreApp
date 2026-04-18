using Microsoft.AspNetCore.Http;

namespace Store.Route.Domains.Exceptions.ValidationError
{
    public class VaslidationErrorResponse
    {
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public string ErrorMessage { get; set; } = "Validation Erroras";

        public IEnumerable<ValidationError> Errors { get; set; }
    }
}
