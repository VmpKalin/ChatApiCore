using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Chat.Data.Context;
using Chat.Logic.Interfaces;
using Chat.Logic.Manages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WebSocketServerChat.IdentityConfig;
using WebSocketServerChat.Midlewares;
using WebSocketServerChat.ProjectExtensions;

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
            var root = Environment.CurrentDirectory;
            var cert = new X509Certificate2(Path.Combine(root, "IdentityConfig", "damienbodserver.pfx"), "");


            services.AddMvc();

            services.AddDbContext<GeneralContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseSuccessEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseErrorEvents = true;
                })
                .AddSigningCredential(cert)
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddCustomUserStore();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = "http://localhost:53261/";
                    options.Audience = "api1";
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.AddSingleton<WebSocketChatManager>();
            services.AddTransient<IWebSocketConnectionManager,WebSocketConnectionManager>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpStatusCodeExceptionMiddleware();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseHttpStatusCodeExceptionMiddleware();
            app.UseAuthentication();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseWebSockets();
            app.Map("/chat", builder =>
               builder.UseMiddleware<SocketMiddleware>(serviceProvider.GetService<WebSocketChatManager>()));

            app.UseMvc();
        }
    }
}
