using API;
using API.Extensions;
using API.Middleware;
using API.SignalR;
using Application.Activities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opts => {
                    var policy = new AuthorizationPolicyBuilder()
                                     .RequireAuthenticatedUser()
                                     .Build();

                    opts.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddFluentValidation(config => 
                {
                    config.RegisterValidatorsFromAssemblyContaining<Create>();
                    config.RegisterValidatorsFromAssemblyContaining<Edit>();
                });

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStaticFiles();

app.UseCors(opts => 
    opts.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseDatabaseMigration();

app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.MapFallbackToController("Index", "Fallback");

app.Run();
