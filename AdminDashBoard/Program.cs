using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities.Identity;
using Store.Route.Persistence;
using Store.Route.Persistence.Data.Contexts;
using Store.Route.Persistence.Identity.Contexts;

namespace AdminDashBoard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region  Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Identity
            builder.Services.AddDbContext<IdentityStoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
            }).AddEntityFrameworkStores<IdentityStoreDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            var app = builder.Build();


            #region Configure the HTTP request pipeline.

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            #endregion

            app.Run();

        }
    }
}