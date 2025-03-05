using EventsWeb.BusinessLogic.Services.Impl;
using EventsWeb.BusinessLogic.Services;
using EventsWeb.DataAccess;
using FluentValidation.AspNetCore;
using FluentValidation;
using EventsWeb.Api;
using EventsWeb.BusinessLogic.MappingProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EventsWeb.Api.Middlewares;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using EventsWeb.DataAccess.Entities;

namespace EventsWeb
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
            builder.Services.AddTransient<IFileService, FileService>();
            builder.Services.AddTransient<IEventService, EventService>();
            builder.Services.AddTransient<IParticipantService, ParticipantService>();
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

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "Images")),
                RequestPath = "/Resources",
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append(
                        "Cache-Control", "public,max-age=600");
                    var corsService = ctx.Context.RequestServices.GetRequiredService<ICorsService>();
                    var corsPolicyProvider = ctx.Context.RequestServices.GetRequiredService<ICorsPolicyProvider>();
                    var policy = corsPolicyProvider.GetPolicyAsync(ctx.Context, "CorsPolicy")
                                    .ConfigureAwait(false)
                                    .GetAwaiter().GetResult();

                    var corsResult = corsService.EvaluatePolicy(ctx.Context, policy);

                    corsService.ApplyResult(corsResult, ctx.Context.Response);
                }
            });


            using var scope = app.Services.CreateScope();

            await FillData.FillDatabaseAsync(scope.ServiceProvider.GetRequiredService<DatabaseContext>(),
                scope.ServiceProvider.GetRequiredService<UserManager<User>>(),
                scope.ServiceProvider.GetRequiredService<IUnitOfWork>());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
