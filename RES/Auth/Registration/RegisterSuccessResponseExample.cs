using SWeb.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SWeb.Docs.AuthExamples.Registration
{
    public class RegisterSuccessResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.Created,
                Message = "User Registered, Check your mail to confirm",
                Body = null
            };
        }
    }
}
