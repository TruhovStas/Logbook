using Logbook.BusinessLogic.Services.Impl;
using Logbook.BusinessLogic.Services;
using Logbook.DataAccess;
using FluentValidation.AspNetCore;
using FluentValidation;
using Logbook.Api;
using Logbook.BusinessLogic.MappingProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Logbook.Api.Middlewares;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Logbook.DataAccess.Entities;

namespace Logbook
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfigureSwagger();
            builder.Services.AddDataAccess(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(IMappingProfile));
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<IValidator>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<IEquipmentService, EquipmentService>();
            builder.Services.AddTransient<IForecastService, ForecastService>();
            builder.Services.AddTransient<ISolutionService, SolutionService>();
            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUser", policy =>
                    policy.RequireAuthenticatedUser());

                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));

            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                var a = builder.Configuration["JwtSettings:SecretKey"];
                var b = builder.Configuration["JwtSettings:Audience"];
                var c = builder.Configuration["JwtSettings:Issuer"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
                };

            });
            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapGet("/", context =>
                {
                    context.Response.Redirect("/Swagger");
                    return Task.CompletedTask;
                });
            }

            app.UseCors("CorsPolicy");

            using var scope = app.Services.CreateScope();

            //await FillData.FillDatabaseAsync(scope.ServiceProvider.GetRequiredService<DatabaseContext>(),
            //    scope.ServiceProvider.GetRequiredService<UserManager<User>>(),
            //    scope.ServiceProvider.GetRequiredService<IUnitOfWork>());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
