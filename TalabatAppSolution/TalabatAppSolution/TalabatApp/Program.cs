
using AutoMapper;
using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Seeding;
using DomainLayer.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Services.AddScoped<ISeddingData, SeedingData>();
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ISeddingData, SeedingData>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

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

            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnect"));
            });
            var app = builder.Build();

            var Scopped = app.Services.CreateScope();

            var ObjectSeeding = Scopped.ServiceProvider.GetRequiredService<ISeddingData>();

            ObjectSeeding.DataSeed();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<CustomExeceptionMiddelwares>();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
