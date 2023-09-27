using Application.Services;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddAppDbContext(configuration)
            .AddTransient<IUnitOfWork, UnitOfWork>()
            .AddRepositories()
            .AddOtherServices();
    }

    private static IServiceCollection AddAppDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("Default")));

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IBookRepository, BookRepository>();

        return services;
    }
    
    private static IServiceCollection AddOtherServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IPasswordManager, PasswordManager>();
        services.AddHttpClient<ITodoApi, TodoApi>(client =>
        {
            client.BaseAddress = 
        });

        return services;
    }
}