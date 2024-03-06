using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.Application.Services;
using ArtworkSharingPlatform.Application.Services.CommissionService;
using ArtworkSharingPlatform.Repository.Interfaces;
using ArtworkSharingPlatform.Repository.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ArtworkSharingPlatform.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IArtworkRepository, ArtworkRepository>();
        services.AddScoped<ICommissionRequestRepository, CommissionRequestRepository>();
        services.AddScoped<IArtworkService, ArtworkServices>();
        services.AddScoped<ICommissionService, CommissionService>();
        return services;
    }
}