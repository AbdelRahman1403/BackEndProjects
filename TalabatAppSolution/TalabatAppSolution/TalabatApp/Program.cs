using AutoMapper;
using Core.DomainLayer.Models.IdentityModels;
using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Seeding;
using DomainLayer.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Perisistence.Repos;
using Perisistence.SeedData;
using Perisistence.Store;
using Perisistence.UOW;
using ServiceAbstractionLayer.IServices;
using ServiceLayer.MappingProfiles;
using ServiceLayer.Services;
using Shared.ErrorModels;
using StackExchange.Redis;
using TalabatApp.CustomMiddelwares;

namespace TalabatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<StoreIdentityDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnect"));
            });
            builder.Services.AddScoped<ISeddingData, SeedingData>();
            builder.Services.AddScoped<ISeedingIdentityData, SeedingIdentityData>();
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ISeddingData, SeedingData>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<ICacheRepo, CacheRepo>();
            builder.Services.AddScoped<ICacheServices, CacheServices>();

            builder.Services.AddIdentity<ApplicationUser , IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.AddAuthentication(configs =>
            {
                configs.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configs.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetSection("JwtOptions")["Issuer"],
                    ValidAudience = builder.Configuration.GetSection("JwtOptions")["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions")["SecurityKey"])),
                };
            });
            builder.Services.AddScoped<IBasketRepo , BasketRepo>();
            builder.Services.AddAutoMapper(prf => prf.AddProfile(new ProductProfile(builder.Configuration)));

            builder.Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var Errors = context.ModelState.Where(e => e.Value.Errors.Any())
                                        .Select(m => new ValidationErrors()
                                        {
                                            Field = m.Key,
                                            Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                                        });
                    var Response = new ValidationErrorToReturn()
                    {
                        Errors = Errors
                    };
                    return new BadRequestObjectResult(Response);
                };
            });

            
            var app = builder.Build();

            var Scopped = app.Services.CreateScope();

            var ObjectSeeding = Scopped.ServiceProvider.GetRequiredService<ISeddingData>();

            ObjectSeeding.DataSeed();

            var ObjectIdentitySeeding = Scopped.ServiceProvider.GetRequiredService<ISeedingIdentityData>();
            ObjectIdentitySeeding.DataSeedAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<CustomExeceptionMiddelwares>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
