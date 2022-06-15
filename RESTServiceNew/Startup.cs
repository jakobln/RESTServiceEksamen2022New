using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTServiceNew
{
    public class Startup
    {
        public const string AllowAllPolicyName = "allowAll";
        public const string AllowOnlyGetMethodPolicyName = "allowOnlyGetMethod";
        public const string AllowOnlyZealandOriginPolicyName = "allowOnlyZealandOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //Having a policy that allows all
            services.AddCors(options => options.AddPolicy(AllowAllPolicyName,
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            //A policy that only allows GET, but everything else
            //We don't use this in this application, this is just an example
            services.AddCors(options => options.AddPolicy(AllowOnlyGetMethodPolicyName,
                builder => builder.AllowAnyOrigin()
                .WithMethods("GET")
                .AllowAnyHeader()));

            //A policy that only allow requests coming from zealand.dk
            services.AddCors(options => options.AddPolicy(AllowOnlyZealandOriginPolicyName,
                builder => builder.WithOrigins("https://zealand.dk")
                .AllowAnyMethod()
                .AllowAnyHeader()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RESTServiceNew", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTServiceNew v1"));
            }

            app.UseRouting();

            //What the default policy should be
            app.UseCors("allowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
