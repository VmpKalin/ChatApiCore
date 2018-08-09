using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;
using Chat.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSocketServerChat.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IHttpContextAccessor _httpContext;

        public UserController(IUserService userService, IHttpContextAccessor context)
        {
            _userService = userService;
            _httpContext = context;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserCreationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.CreateUser(model);

            if (result.Error!=null)
                return BadRequest(result);

            return Ok(result.Data);
        }


        [HttpPost("CreateUserInfo/{userId}")]
        public async Task<IActionResult> CreateUserInfo([FromRoute]string userId, [FromBody] UserInfoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.AddUserInfo(userId,model);

            if (result.Error!=null)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }


        [HttpGet("Autho")]
        [Authorize]
        public IActionResult GetAutho()
        {
            return Ok("Authorized");
        }

        [HttpDelete("Delete")]
        public async Task DeleteUsers()
        {
            await _userService.EraseDb();
        }


        [HttpGet("Free")]
        public IActionResult GetFree()
        {
            return Ok("Unregistred user");
        }


        [HttpGet("All")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetUsers());
        }

        [Authorize]
        [HttpGet("Info")]
        public async Task<ActionResult> UserInfo()
        {
            var idFromClaims = User.Claims.Single(x => x.Properties.Values.Contains("sub")).Value;

            if (string.IsNullOrEmpty(idFromClaims))
                return BadRequest("Can`t find user id");

            var responce = await _userService.GetUser(x => x.Id == idFromClaims);

            if (responce.Error != null)
            {
                return BadRequest(responce.Error);
            }

            return Ok(responce.Data);
        }

        [HttpGet("Claims")]
        public ActionResult GetClaims()
        {
            return new JsonResult(User.Claims.Select(
                c => new { c.Type, c.Value }));
        }
    }
}
