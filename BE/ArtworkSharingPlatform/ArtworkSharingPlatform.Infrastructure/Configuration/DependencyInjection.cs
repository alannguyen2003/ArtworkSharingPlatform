using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.Application.Services;
using ArtworkSharingPlatform.Application.Services.CommissionService;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Repository.Interfaces;
using ArtworkSharingPlatform.Repository.Repository;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;

namespace ArtworkSharingPlatform.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IArtworkRepository, ArtworkRepository>();
        services.AddScoped<ICommissionRequestRepository, CommissionRequestRepository>();
        services.AddScoped<ICommissionImagesRepository, CommissionImagesRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ICommissionStatusRepository, CommissionStatusRepository>();
        services.AddScoped<IArtworkService, ArtworkServices>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<ICommissionService, CommissionService>();
        services.AddScoped<IPackageRepository, PackageRepository>();
        services.AddScoped<IPackageService, PackageService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IConfigRepository, ConfigRepository>();
        services.AddScoped<IConfigService, ConfigService>();
        return services;
    }
}