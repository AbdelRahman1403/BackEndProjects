using ApplicationLayer.Behaviors;
using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces.Repositories;
using ApplicationLayer.Interfaces.Services;
using ApplicationLayer.Interfaces.UOW;
using ApplicationLayer.Seeding;
using FluentValidation;
using HealthCareSystem.CustomMiddelwares;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using Persistence.Reposetories;
using Persistence.Services;
using Persistence.UOW;
using Shared.ErrorModels;
using Shared.OptionsModels;
using StackExchange.Redis;
using System.Text;

namespace HealthCareSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<HealthCareDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Configuration.GetConnectionString("RedisConnect");
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var connectionString =
                    builder.Configuration
                        .GetConnectionString("RedisConnect");

                var configurationOptions =
                    ConfigurationOptions.Parse(connectionString!);

                configurationOptions.AbortOnConnectFail = false;

                return ConnectionMultiplexer.Connect(configurationOptions);
            }); builder.Services
                .AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<HealthCareDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(ApplicationLayer.IAssemblyMarker).Assembly));
            builder.Services.AddValidatorsFromAssembly(typeof(ApplicationLayer.IAssemblyMarker).Assembly);


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IEmailService, EmailServices>();
            builder.Services.AddScoped<IOTPService, OTPService>();
            builder.Services.AddScoped<IJWTService, JWTService>();
            builder.Services.AddScoped<ICasheReposetory , CasheReposetory>();
            //builder.Services.AddScoped<IGenericReposetory , GenericReposetory>();
            builder.Services.AddScoped<IotpCodeReposetory , otpCodeReposetory>();
            builder.Services.AddScoped<IRegistrationSessionRepository, RegistrationSessionRepository>();

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            builder.Services.Configure<EmailSettingsOptions>(builder.Configuration.GetSection(EmailSettingsOptions.EmailSettings));
            var jwtOptions = builder.Configuration.GetSection(JWTSettingsOptions.JWTSettings).Get<JWTSettingsOptions>();
            builder.Services.Configure<JWTSettingsOptions>(builder.Configuration.GetSection(JWTSettingsOptions.JWTSettings));

            builder.Services
                            .AddAuthentication(
                                JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters =
                                    new TokenValidationParameters
                                    {
                                        ValidateIssuer = true,
                                        ValidateAudience = true,
                                        ValidateLifetime = true,
                                        ValidateIssuerSigningKey = true,

                                        ValidIssuer = jwtOptions.Issuer,
                                        ValidAudience = jwtOptions.Audience,

                                        IssuerSigningKey =
                                            new SymmetricSecurityKey(
                                                Encoding.UTF8.GetBytes(
                                                    jwtOptions.key))
                                    };
                            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.Configure<ApiBehaviorOptions>(options =>
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


            app.MapControllers();

            app.Run();
        }
    }
}
