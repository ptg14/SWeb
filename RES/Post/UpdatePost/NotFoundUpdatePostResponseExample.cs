﻿namespace SWeb.Docs.PostExamples.UpdatePost
{
    using SWeb.Utils;
    using Swashbuckle.AspNetCore.Filters;
    using System.Net;

    public class NotFoundUpdatePostResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Post not found.",
                StatusCode = HttpStatusCode.NotFound
            };
        }
    }
}
