
using System.Text;
using API.Services;
using Application.Core;
using Domain;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
                                                              IConfiguration config) 
    {
        services.AddIdentityCore<AppUser>(opts => 
        {
            opts.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<DataContext>()
        .AddSignInManager<SignInManager<AppUser>>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts => 
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };

                    opts.Events = new JwtBearerEvents 
                    {
                        OnMessageReceived = context => 
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;

                            if(!string.IsNullOrEmpty(path) && path.StartsWithSegments("/chat"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

        services.AddAuthorization(opts => 
        {
            opts.AddPolicy(AuthorizationPolicies.IsActivityHost, policy => 
            {
                policy.Requirements.Add(new IsHostRequirment());
            });
        });

        services.AddTransient<IAuthorizationHandler, IsHostRequirmentHandler>();
        services.AddScoped<TokenService>();

        return services;
    }
}