using SWeb.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SWeb.Docs.AuthExamples.Verification
{
    public class VerifyInternalServerErrorResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Internal Server Error, Try again later",
                Body = null
            };
        }
    }

}
