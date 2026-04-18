using Store.Route.Domains.Contracts;
using Store.Route.Web.Extentions;
using Store.Route.Web.Middelwares;
namespace Store.Route.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAllServices(builder.Configuration);

            var app = builder.Build();



            // Ask From CLR
            #region Initialize DB

            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();  // Ask From CLR To Create Object From IDbInitializer  
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();


            #endregion

            app.UseMiddleware<GlobalErrorHandlingMiddelware>();

            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
