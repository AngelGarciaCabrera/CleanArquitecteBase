
using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;


public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services){
        
        services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>());
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviors<,>));

        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();
        return services;
    }
}