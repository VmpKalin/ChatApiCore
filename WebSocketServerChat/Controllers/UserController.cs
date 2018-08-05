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
        public async Task<IActionResult> Register([FromBody] UserCreateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.CreateUser(model);

            if (!result)
                return BadRequest();

            return Ok();
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
            await _userService.DeleteUsers();
        }


        [HttpGet("Free")]
        public IActionResult GetFree()
        {
            return Ok("Unregistred user");
        }


        [HttpGet("All")]
        public async Task<IEnumerable<UserEntity>> Get()
        {
            return await _userService.GetUsers();
        }

        [Authorize]
        [HttpGet("Info")]
        public async Task<ActionResult> UserInfo()
        {
            var idFromClaims = User.Claims.Single(x => x.Properties.Values.Contains("sub")).Value;

            var userInfo = await _userService.GetUsers(x => x.Id == idFromClaims);

            return Ok(userInfo);
        }

        [HttpGet("Claims")]
        public ActionResult GetClaims()
        {
            return new JsonResult(User.Claims.Select(
                c => new { c.Type, c.Value }));
        }
    }
}
