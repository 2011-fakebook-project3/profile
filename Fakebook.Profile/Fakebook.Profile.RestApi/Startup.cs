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

namespace Fakebook.Profile.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            var connectionString = Configuration.GetConnectionString("default");
            if (connectionString is null)
            {
                throw new InvalidOperationException("no connection string 'default' found");
            }
            services.AddDbContext<ProfileDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IProfileRepository, ProfileRepository>();
          
            services.AddControllers();

            /*
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fakebook.ProfileRestApi", Version = "v1" });
            });
            */

            services.AddDbContext<ProfileDbContext>(options
                => options.UseNpgsql(Configuration["FakebookProfile:ConnectionString"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fakebook.ProfileRestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
