using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Data.Context;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;
using Chat.Data.Models.Entities.LikeModels;
using Chat.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebSocketServerChat.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        
        // GET: api/Post
        [HttpGet("PostByUserId/{userId}")]
        public async Task<IActionResult> GetPostByUserId(string userId)
        {
            if(string.IsNullOrEmpty(userId))
                return BadRequest("id is empty!");

            var result = await _postService.GetPostByUserId(userId);

            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        // GET: api/Post/5
        [HttpGet("PostByPostId/{id}")]
        public async Task<IActionResult> GetPost(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("id is empty!");

            var result = await _postService.GetPostById(id);

            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);

        }
        
        // POST: api/Post
        [HttpPost("CreatePost")]
        public async Task<IActionResult> Post([FromBody] PostDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _postService.CreatePost(model);

            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }
    }
}
