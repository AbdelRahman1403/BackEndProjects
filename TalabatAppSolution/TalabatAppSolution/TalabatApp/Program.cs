
using AutoMapper;
using DomainLayer.Seeding;
using DomainLayer.UOW;
using Microsoft.EntityFrameworkCore;
using Perisistence.SeedData;
using Perisistence.Store;
using Perisistence.UOW;
using ServiceAbstractionLayer.IServices;
using ServiceLayer.MappingProfiles;
using ServiceLayer.Services;

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

            builder.Services.AddAutoMapper(prf => prf.AddProfile(new ProductProfile(builder.Configuration)));

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

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
