using SWeb.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SWeb.Docs.AuthExamples.Login
{
    public class LoginNotFoundErrorResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "User not found",
                Body = null
            };
        }
    }

}
