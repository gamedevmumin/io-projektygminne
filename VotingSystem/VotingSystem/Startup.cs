using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Controllers;
using VotingSystem.CronJobs;
using VotingSystem.ExternalServices;
using VotingSystem.MappingProfiles;
using VotingSystem.Middleware;
using VotingSystem.Models;
using VotingSystem.Models.Data;

namespace VotingSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<VotingSystemDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });

            services.AddAutoMapper(options =>
            {
                options.AddProfile<DefaultMappingProfile>();
            });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var appSecret = Configuration["AppSecret"];
                    var secretBytes = Encoding.UTF8.GetBytes(appSecret);
                    var signingKey = new SymmetricSecurityKey(secretBytes);

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            IssuerSigningKey = signingKey,
                            ValidAlgorithms = new string[] { SecurityAlgorithms.HmacSha256 },
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = false,
                            ClockSkew = TimeSpan.Zero
                        };
                });

            services.AddCronJob<ConcludeEditionCronService>(options =>
            {
                options.CronExpression = @"*/10 * * * *";
            });

            services.AddScoped<IVotingSystemDb, VotingSystemDb>();
            services.AddScoped<IFileHostingService, LocalFileHostingService>();
            services.AddScoped<IMailingService, MailingService>();
            services.AddScoped<IPeselService, MockPeselService>();

            services.AddCors(o => o.AddPolicy("DefaultCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddTransient<AuthMiddleware>();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("DefaultCorsPolicy");

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<AuthMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
