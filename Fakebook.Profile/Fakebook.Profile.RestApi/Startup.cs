using System;
using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Fakebook.Profile.DataAccess.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Okta.AspNetCore;
using Fakebook.Profile.DataAccess.Services.Interfaces;

namespace Fakebook.Profile.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            services.AddAuthentication(                
                JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://dev-7862904.okta.com/oauth2/default";
                    options.Audience = "api://default";
                    //Won't send details outside of dev env
                    if (_env.IsDevelopment())
                    {
                        options.IncludeErrorDetails = true;
                    }                   
                    options.RequireHttpsMetadata = false;
                }).AddOktaMvc(new OktaMvcOptions
                    {
                        OktaDomain = "https://dev-7862904okta.com/oauth2/default",
                        ClientId = "CLIENT_ID_HERE",
                        ClientSecret = "CLIENT_SECRET_HERE",
                    }
                );

            services.AddControllers();

            /*
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fakebook.ProfileRestApi", Version = "v1" });
            });
            */

            services.AddDbContext<ProfileDbContext>(options
                => options.UseNpgsql(Configuration["FakebookProfile:ConnectionString"]));

            services.AddTransient<IStorageService, StorageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app) {
            if (_env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fakebook.ProfileRestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
