using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.API.Services.Conversations;
using SocialNetwork.API.Services.FriendRequests;
using SocialNetwork.API.Services.Messages;
using SocialNetwork.API.Services.Users;
using SocialNetwork.Authentification;
using SocialNetwork.BLL.Conversations;
using SocialNetwork.BLL.FriendRequests;
using SocialNetwork.BLL.Messages;
using SocialNetwork.BLL.Users;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.UnitOfWork;
using Swashbuckle.AspNetCore.Swagger;

namespace SocialNetwork.API
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection ResolveDalDependencies(this IServiceCollection services, string conString)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(conString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFriendRequestService, FriendRequestService>();
            
            services.AddScoped<IMessageResponseCreator, MessageResponseCreator>();
            services.AddScoped<IConversationResponseCreator, ConversationResponseCreator>();
            services.AddScoped<IUserResponseCreator, UserResponseCreator>();
            services.AddScoped<IFriendRequestResponseCreator, FriendRequestResponseCreator>();

            return services;
        }
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "SocialNetwork",
                    Version = "v1"
                });
                //c.IncludeXmlComments(
                //    @"bin\Debug\netcoreapp2.0\SocialNetwork.API.xml");
            });

            return services;
        }

        public static IServiceCollection ResolveIdentityDependencies(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<Authentification.User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            return services;
        }
    }
}
