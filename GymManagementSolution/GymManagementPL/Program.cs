using BLL.AttachmentServices;
using BLL.Interfaces;
using BLL.Profiles;
using BLL.Serveices;
using DAL.Data;
using DAL.Models.IdentityModels;
using DAL.Reposatories.Interfaces;
using DAL.Reposatories.InterfacesRepos;
using DAL.Reposatories.Repos;
using DAL.SeedingData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                //var connectionString = builder.Configuration.GetConnectionString("GymDbConnection");
                //var connteictionString = builder.Configuration.GetSection("ConnectionStrings")["DefualtConntection"];
                var connteictionString = builder.Configuration["ConnectionStrings:DefualtConntection"];
                options.UseSqlServer(connteictionString);
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<GymDbContext>();

            builder.Services.AddScoped(typeof(IGenericRepo<>) , typeof(GenericRepo<>));
            builder.Services.AddScoped<ISessionRepo, SessionRepo>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAnalyticsService , AnalyticsServices>();
            builder.Services.AddScoped<IMemberServices , MemberServices>();
            builder.Services.AddScoped<ITrainerServices , TrainerServices>();
            builder.Services.AddScoped<IPlanServices, PlanServices>();
            builder.Services.AddScoped<ISessionServices, SessionServices>();
            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();
            builder.Services.AddScoped<IAccountServices, AccountServices>();
            builder.Services.AddAutoMapper(mp => mp.AddProfile(new SessionProfile()));
            builder.Services.AddAutoMapper(mp => mp.AddProfile(new MemberProfile()));
            builder.Services.AddAutoMapper(mp => mp.AddProfile(new TrainerProfile()));





            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var services = Scope.ServiceProvider.GetRequiredService<GymDbContext>();
            var userManager = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var pandingMigrations = services.Database.GetPendingMigrations();
            if(pandingMigrations.Any())
            {
                services.Database.Migrate();
            }

            SeedingData.SeedData(services);
            IdentityDbContextSeedingData.SeedData(roleManager , userManager);



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
