using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities.Identity;
using Store.Route.Domains.Exceptions.ValidationError;
using Store.Route.Persistence;
using Store.Route.Persistence.Identity.Contexts;
using Store.Route.Services;
using Store.Route.Shared;
using Store.Route.Web.Middelwares;
using System.Text;

namespace Store.Route.Web.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddWebServices();
            service.AddIdentityServices();

            service.AddInfrastructureServices(configuration);

            service.AddApplicationServices(configuration);

            service.ConfigureApiBehaviorOptions();

            service.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));

            service.AddAuthenticationService(configuration);

            service.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                                              .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return service;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection service)
        {
            service.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();

            return service;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection service)
        {
            service.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<IdentityStoreDbContext>();

            return service;
        }

        private static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection service)
        {
            service.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                 .Select(m => new ValidationError()
                 {
                     Field = m.Key,
                     Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)

                 });
                    var response = new VaslidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };

            });

            return service;
        }
        private static IServiceCollection AddAuthenticationService(this IServiceCollection service, IConfiguration configuration)
        {

            var jwtOptions = configuration.GetSection("JWTOptions").Get<JWTOptions>();

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
                };
            });

            return service;
        }



        public static async Task<WebApplication> ConfigureMiddelware(this WebApplication app)
        {
            await app.SeedData();
            app.UseGlobalErroHandleng();

            app.UseStaticFiles();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseResponseCaching();


            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }




        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();  // Ask From CLR To Create Object From IDbInitializer  
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
            return app;
        }

        private static WebApplication UseGlobalErroHandleng(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddelware>();

            return app;
        }

    }

}
