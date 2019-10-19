using System.Net.Http.Formatting;
using System.Text;
using GamerPalsBackend.DataObjects.Models;
using GamerPalsBackend.Other;
using GamerPalsBackend.Policies;
using GamerPalsBackend.WebSockets;
using log4net.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GamerPalsBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            byte[] key = Encoding.UTF8.GetBytes(PalsConfiguration.SystemSettings["JWTSecret"]);
            services.AddMvc().AddJsonOptions(
                options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                });
            services.AddSwaggerDocument(c => c.Version = "v1.1");
            services.TryAddSingleton(typeof(IUserIdProvider), typeof(DefaultUserIdProvider));
            services.AddSignalR();

            services.AddTransient(_ => new MongoContext());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization(a => a.AddPolicy("IsOwnerPolicy", policy => policy.Requirements.Add(new IsOwnerPolicyRequirements())));
            services.AddAuthorization(a => a.AddPolicy("IsInFriendsListPolicy", policy => policy.Requirements.Add(new IsInFriendsListPolicyRequirements())));
            services.AddSingleton<IAuthorizationHandler, IsOwnerPolicyHandler>();
            services.AddSingleton<IAuthorizationHandler, IsInFriendsListPolicyHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseSignalR(conf => conf.MapHub<NotificationHub>("/Hub"));
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
