using Application;
using FluentValidation;
using Web.API.Middlewares;

namespace web.API;


public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        //aqui paso los servicios que utilizo en el program.cs
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        services.AddTransient<GlobalExceptionHandkingMiddleware>();
        
        return services;
    }
}