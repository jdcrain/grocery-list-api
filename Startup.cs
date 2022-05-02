using GroceryListApi.Models;
using GroceryListApi.Repositories.GroceryList;
using GroceryListApi.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GroceryListApi
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
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddDbContext<AppDbContext>(opt => {
                opt.UseNpgsql(Configuration.GetConnectionString("GroceryListApiConnection"));
            });

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT_SECRET"])); // create a signing key so that we know we created the JWT
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // add signing key and credentials as singletons to use the same values across all requests
            services.AddSingleton(signingKey);
            services.AddSingleton(creds);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false, // secret key may be shared with other services in our application, so set to false
                        ValidateAudience = false, // secret key may be shared with other services in our application, so set to false
                        ValidateLifetime = true, // verify token hasn't expired
                        ValidateIssuerSigningKey = true, // validate issuer signing key so we know the key is coming from the secret value
                        IssuerSigningKey = signingKey
                    };
                });

            services.AddCors();

            #region Repositories
            services.AddTransient<IGroceryListRepository, GroceryListRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(a => 
                a.SetIsOriginAllowedToAllowWildcardSubdomains()
                .WithOrigins(Configuration.GetConnectionString("AllowedOrigins").Split(","))
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
