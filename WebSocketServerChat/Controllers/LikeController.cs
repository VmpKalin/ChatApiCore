using System.Linq;
using System.Threading.Tasks;
using Chat.Data.Context;
using Chat.Data.Models.Entities.LikeModels;
using Chat.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebSocketServerChat.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class LikeController : Controller
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpGet("Like/{postId}")]
        public async Task<IActionResult> Get(string postId)
        {
            var result =await _likeService.GetLikesByPostId(postId);

            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpPost("Like/Create")]
        public async Task<IActionResult> Post(string postId, string userLikeFromId)
        {
            var result = await _likeService.CreateLike(postId, userLikeFromId);

            if(result.Error != null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }




        //[HttpGet("Like/Post/{id}")]
        //public async Task<IActionResult> Image(string id)
        //{
        //    var bytes = new byte[256];


        //    var stream = new MemoryStream(bytes);

        //    var image = new KalikoImage("");

        //    return;
        //}
    }
}
