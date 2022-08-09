using Application.Activities;
using Application.Core;
using Application.Interfaces;
using Infrastructure.Photos;
using Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{

    private static void AddMediator<T>(IServiceCollection services)
        where T : class
    {
        services.AddMediatR(typeof(T).Assembly);
    }

    private static void AddAutoMapper<T>(IServiceCollection services)
        where T : class
    {
        services.AddAutoMapper(typeof(T).Assembly);
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddDbContext<DataContext>(opts =>
            opts.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddCors();

        AddAutoMapper<MappingProfiles>(services);

        AddMediator<List.Query>(services);

        AddMediator<Details.Query>(services);

        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddScoped<IPhotoAccessor, PhotoAccessor>();

        services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));

        services.AddSignalR();

        return services;
    }
}