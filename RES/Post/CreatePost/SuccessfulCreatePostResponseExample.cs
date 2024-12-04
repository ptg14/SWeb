using SWeb.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SWeb.Docs.PostExamples.CreatePost
{
    public class SuccessfulCreatePostResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Post created successfully",
                StatusCode = HttpStatusCode.Created
            };
        }
    }
}
