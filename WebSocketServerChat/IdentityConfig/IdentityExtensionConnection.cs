using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Logic.Interfaces;
using Chat.Logic.Manages;
using Microsoft.Extensions.DependencyInjection;

namespace WebSocketServerChat.IdentityConfig
{
    public static class IdentityExtensionConnection
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IUserService, UserService>();
            builder.AddProfileService<CustomProfileService>();
            builder.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}
