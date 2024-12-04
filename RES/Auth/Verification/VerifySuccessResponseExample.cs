using SWeb.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SWeb.Docs.AuthExamples.Verification
{
    public class VerifySuccessResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Email Confirmed!",
                Body = null
            };
        }
    }

}
