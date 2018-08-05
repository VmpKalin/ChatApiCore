using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chat.Logic.Interfaces;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace WebSocketServerChat.IdentityConfig
{
    public class CustomProfileService : IProfileService
    {
        protected readonly IUserService _userRepository;

        public CustomProfileService(IUserService userRepository, ILogger<CustomProfileService> logger)
        {
            _userRepository = userRepository;
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = await _userRepository.FindUserById(context.Subject.GetSubjectId());

            var claims = new List<Claim>
            {
                new Claim("role", "dataEventRecords.admin"),
                new Claim("role", "dataEventRecords.user"),
                new Claim("login", user.Login),
                new Claim("email", user.Email)
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userRepository.FindUserById(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }
    }
}
