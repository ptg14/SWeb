﻿namespace SWeb.Docs.PostExamples.GetUserHomePosts
{
    using SWeb.Utils;
    using Swashbuckle.AspNetCore.Filters;
    using System.Net;

    public class InternalServerErrorGetUserHomePostsResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Internal Server Error, Try again later",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}
