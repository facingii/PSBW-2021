using EmployeesMicroService.Bussiness;
using EmployeesMicroService.Helpers;
using EmployeesMicroService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EmployeesMicroService
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
            services.AddControllers();

            // add swagger package to autogenerate documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeesMicroService", Version = "v1" });
            });

            // add entity framework context
            services.AddDbContext<Models.employeesContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("EmployeesConnectionString");
                var version = ServerVersion.Parse("8.0.26-mysql");
                options.UseMySql(connectionString, version);
            });

            // extracting jwt secret from config file
            var jwtSection = Configuration.GetSection("JwtSettings");
            var jwtSettings = jwtSection.Get<JwtSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes (jwtSettings.Secret);

            // add JwtSettigs object as configuration not service
            services.Configure<JwtSettings>(jwtSection);

            // add Jwt authentication
            services.AddAuthentication(authOptions =>
            {
               authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
               bearerOptions.RequireHttpsMetadata = false;
               bearerOptions.SaveToken = true;
               bearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
            });

            services.AddCors (options =>
            {
                options.AddPolicy ("MY_CORS", builder =>
                {
                    builder.WithOrigins ("http://localhost:3000");
                    builder.AllowAnyMethod ();
                    builder.AllowAnyHeader ();
                });
            });

            // add custom services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeesMicroService v1"));
            }

            app.UseRouting ();
            app.UseCors ("MY_CORS");
            app.UseAuthentication ();
            app.UseAuthorization ();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
