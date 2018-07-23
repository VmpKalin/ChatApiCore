using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Logic.Interfaces;
using Chat.Logic.Manages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebSocketServerChat.Midlewares;

namespace WebSocketServerChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<WebSocketChatManager>();
            services.AddTransient<IWebSocketConnectionManager,WebSocketConnectionManager>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.Map("/chat", builder =>
               builder.UseMiddleware<SocketMiddleware>(serviceProvider.GetService<WebSocketChatManager>()));

            app.UseMvc();
        }
    }
}
