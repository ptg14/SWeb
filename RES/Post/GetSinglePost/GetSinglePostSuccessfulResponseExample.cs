﻿using SWeb.Utils;
using SWeb.ViewModels;
using Swashbuckle.AspNetCore.Filters;

namespace SWeb.Docs.PostExamples.GetSinglePost
{
    public class GetSinglePostSuccessfulResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = new ReadPostViewModel
                {
                    Content = "This is a post",
                    UserId = 1,
                    PublishedOn = DateTime.Now
                },
                Message = "Post retrieved successfully",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
