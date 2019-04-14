using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SocialNetwork.API.Services.Conversations;
using SocialNetwork.API.Services.Messages;
using SocialNetwork.BLL.Conversations;
using SocialNetwork.BLL.Messages;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.MessageRepository;
using SocialNetwork.DAL.UnitOfWork;


namespace SocialNetwork.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.ResolveDalDependencies(Configuration.GetConnectionString("SocialNetwork"));
            services.ResolveServicesDependencies();
            services.ResolveIdentityDependencies(Configuration.GetConnectionString("SocialNetworkAuthentication"));

            services.RegisterSwagger();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Social Network API v1"));

            app.UseMvc();
            
        }
    }
}
