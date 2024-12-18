﻿using SWeb.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SWeb.Docs.AuthExamples.Login
{
    public class LoginValidationErrorResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Invalid data!",
                Body = new Dictionary<string, string>
            {
                { "Email", "The email field is required." },
                { "Password", "The password field is required." }
            }
            };
        }
    }

}
