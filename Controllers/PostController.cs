﻿using Microsoft.AspNetCore.Mvc;
using SWeb.Docs.PostExamples.CreatePost;
using SWeb.Docs.PostExamples.DeletePost;
using SWeb.Docs.PostExamples.GetSinglePost;
using SWeb.Docs.PostExamples.GetUserHomePosts;
using SWeb.Docs.PostExamples.GetUserPosts;
using SWeb.Docs.PostExamples.UpdatePost;
using SWeb.Exceptions;
using SWeb.Services;
using SWeb.Utils;
using SWeb.ViewModels;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SWeb.Controllers
{
    [Route("api/v1/posts")]
    public class PostController
    {
        private PostService _postService;
        public PostController(PostService postService)
        {
            _postService = postService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SuccessfulCreatePostResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundCreatePostResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorCreatePostResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> CreatePost([FromForm] SavePostViewModel postViewModel)
        {
            try
            {
                await _postService.AddPost(postViewModel);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Post created successfully";
                apiResponse.StatusCode = HttpStatusCode.Created;
                return apiResponse;
            }
            catch(NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;

            }
            catch
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }


        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<Object>>> GetAllPosts()
        {
            try
            {
                List<ReadPostViewModel> posts = await _postService.GetAllPosts();
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = posts;
                apiResponse.Message = "Posts fetched successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }



        [HttpGet("user/{id}")]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserPostsSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundGetUserPostsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorGetUserPostsResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> GetUserPosts(int id)
        {
            try
            {
                List<ReadPostViewModel> post = await _postService.GetPostsByUserId(id);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = post;
                apiResponse.Message = "Post fetched successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch (NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
            catch
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetSinglePostSuccessfulResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(PostNotFoundResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(GetSinglePostInternalServerErrorResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> GetPost(int id)
        {
            try
            {
                ReadPostViewModel post = await _postService.GetPostById(id);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = post;
                apiResponse.Message = "Post fetched successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch (NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
            catch
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }
        
        [HttpGet("home/{id}")]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessfulGetUserHomePostsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundGetUserHomePostsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorGetUserHomePostsResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> GetUserHomePosts(int id)
        {
            try
            {
                List<ReadPostViewModel> posts = await _postService.GetPostsByFollowing(id);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = posts;
                apiResponse.Message = "Posts of following list fetched successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch (NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
            catch
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessfulUpdatePostResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundUpdatePostResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorUpdatePostResponseExample))]

        public async Task<ActionResult<ApiResponse<Object>>> UpdatePost(int id, [FromBody] UpdatePostViewModel post)
        {
            try
            {
                await _postService.UpdatePost(id, post);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Post updated successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch (NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
            catch
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessfulDeletePostResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundDeletePostResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorDeletePostResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> DeletePost(int id)
        {
            try
            {
                await _postService.DeletePost(id);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Post deleted successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch (NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
            catch
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }


        // Comment Endpoints
        [HttpPost("comments")]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SuccessfulCreateCommentResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundCreateCommentResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorCreateCommentResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> AddCommentToPost([FromBody] SaveCommentViewModel comment)
        {
            try
            {
                await _postService.AddCommentToPost(comment);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Comment added successfully";
                apiResponse.StatusCode = HttpStatusCode.Created;
                return apiResponse;
            }
            catch (NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
            catch
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

        [HttpPut("{postId}/comments")]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessfulUpdateCommentResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundUpdateCommentResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorUpdateCommentResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> UpdateCommentInPost(int postId, [FromBody] ChangedCommentViewModel comment)
        {
            try
            {
                await _postService.UpdateCommentInPost(postId, comment);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Comment updated successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch (NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
            catch (Exception e)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

        [HttpDelete("{postId}/comments")]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessfulDeleteCommentResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundDeleteCommentResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorDeleteCommentResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> DeleteCommentFromPost(int postId, [FromBody] DeleteCommentViewModel comment)
        {
            try
            {
                await _postService.DeleteCommentFromPost(postId, comment);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Comment deleted successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch (NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
            catch (Exception e)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

    }
}
