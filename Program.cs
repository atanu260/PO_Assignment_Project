using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PO_Assignment_Project.Data;
using System;
using System.Threading.Tasks;

namespace PO_Assignment_Project
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("POAsssignment"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);

               // builder.Services.AddControllersWithViews();
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>();

            var app = builder.Build();


            //using(var scope = app.Services.CreateAsyncScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //    var roles = Enum.GetNames(typeof(PO_Assignment_Project.Data.Enum.UserType));
            //    foreach (var role in roles)
            //    {
            //        if(!await roleManager.RoleExistsAsync(role))
            //        {
            //            await roleManager.CreateAsync(new IdentityRole(role));
            //        }
            //    }
            //}

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            using (var scope = app.Services.CreateAsyncScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = Enum.GetNames(typeof(PO_Assignment_Project.Data.Enum.UserType));
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            app.Run();
        }
    }
}
