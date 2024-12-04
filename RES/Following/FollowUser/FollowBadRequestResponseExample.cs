﻿namespace SWeb.Docs.FollowingExamples.FollowUser
{
    using SWeb.Utils;
    using Swashbuckle.AspNetCore.Filters;

    public class FollowBadRequestResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "You are already following this user.",
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

}
