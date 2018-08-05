using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Logic.Interfaces;
using IdentityModel;
using IdentityServer4.Validation;

namespace WebSocketServerChat.IdentityConfig
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserService _userRepository;

        public CustomResourceOwnerPasswordValidator(IUserService userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (await _userRepository.ValidateCredentials(context.UserName, context.Password))
            {
                var user = await _userRepository.FindByUsername(context.UserName);
                context.Result = new GrantValidationResult(user.Id, OidcConstants.AuthenticationMethods.Password);
            }
        }
    }
}
