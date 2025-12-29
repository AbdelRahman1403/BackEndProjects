using DAL.Data;
using DAL.Reposatories.Interfaces;
using DAL.Reposatories.Repos;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddScoped(typeof(IGenericRepo<>) , typeof(GenericRepo<>));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
