﻿using SWeb.Utils;
using Swashbuckle.AspNetCore.Filters;

public class UnfollowNotFoundResponseExample : IExamplesProvider<ApiResponse<object>>
{
    public ApiResponse<object> GetExamples()
    {
        return new ApiResponse<object>
        {
            Body = null,
            Message = "User not found",
            StatusCode = System.Net.HttpStatusCode.NotFound
        };
    }
}
