﻿using SWeb.Utils;
using Swashbuckle.AspNetCore.Filters;

namespace SWeb.Docs.PostExamples.GetSinglePost
{
    public class PostNotFoundResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Post not found",
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }
}
