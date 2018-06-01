using System.Text;
using GenesisAutomation.EF;
using GenesisAutomation.Interfaces;
using GenesisAutomation.Logging;
using GenesisAutomation.Repositories;
using GenesisAutomation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GenesisAutomation
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
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("DataContext"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidAudience = Configuration["Jwt:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
                });

            services.AddMvc();

            //Services
            services.AddTransient<ISignUpService, SignUpService>();
            services.AddTransient<ISignInService, SignInService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ISearchService, SearchService>();

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();

            //Logging
            services.AddTransient<ICILogger, FileLogger>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();

        }


    }
}
