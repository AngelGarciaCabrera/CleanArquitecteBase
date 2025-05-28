using System.Security.AccessControl;
using Infraestructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Data;
using Domain.Primitives;
using Persistence.Repositories;
using Domain.Usuario;
using Infraestructure.Persistence.Repositories;
using Application.EnviarCorreo.UserCorreo;
using Application.EnviarCorreo;
using Domain.Tickets;
 
using Application.EnviarCorreo.NewTIcketEmail;

namespace Infraestructure;
public static class DependencyInjection
{
    //el iConfiguratuión es para poder acceder a los appsettings.json
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //al db context le asignamos la cadena de conexion que esta en appsettings.json para que use sql
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());


        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        services.AddScoped<ITicketsRepository, TicketsRepository>();
        //añado la depnendencias del los servicio de correo
        services.AddScoped<NewTIcketEmailServices>();
        services.AddScoped<Email_User_Service>();
        services.AddScoped<Email_Interal_Service>();


        return services;
    }
}



