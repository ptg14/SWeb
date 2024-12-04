﻿using SWeb.Utils;
using Swashbuckle.AspNetCore.Filters;

public class UnfollowInternalServerErrorResponseExample : IExamplesProvider<ApiResponse<object>>
{
    public ApiResponse<object> GetExamples()
    {
        return new ApiResponse<object>
        {
            Body = null,
            Message = "Internal Server Error, Try again later",
            StatusCode = System.Net.HttpStatusCode.InternalServerError
        };
    }
}
