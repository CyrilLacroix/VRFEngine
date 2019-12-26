using VRFEngine.Common.Service;
using VRFEngine.Common.Settings;
using VRFEngine.Service.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace VRFEngine.Api
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
            // services.AddSingleton<IHttpContextAccessor, FakeHttpContextAccessor>();

            services.AddCors();

            services.AddControllers().AddNewtonsoftJson();

            services.Configure<Settings>(Configuration.GetSection("Settings"));

            services.AddVRFEngineServices(Configuration.GetConnectionString("DefaultConnection"));

            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment())
            {
                app.UseCors(builder =>
                    builder.WithOrigins("http://localhost:8080")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            }

            app.UseStaticFiles();

            // Register the Swagger generator and the Swagger UI middlewares
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Home");
            });
        }
    }
}
