using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Yow.Api.Configurations.AutoMapper;
using static Yow.Api.Configurations.FluentValidation;
using static Yow.Api.Configurations.MediatR;
using static Yow.Api.Configurations.Mvc;
using static Yow.Api.Configurations.Swagger;

namespace Yow.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterMediatR(services);
            RegisterAutoMapper(services);
            IMvcBuilder mvcBuilder = RegisterMvc(services);
            AddFluentValidation(mvcBuilder);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Console.WriteLine("Current Environment");
            Console.WriteLine(env.EnvironmentName);
     
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }  

            ConfigureSwagger(app, env);
            ConfigureMvc(app);   
        }
    }
}