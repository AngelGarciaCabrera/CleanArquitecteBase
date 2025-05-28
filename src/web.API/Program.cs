using Application;
using Infraestructure;
using Microsoft.Identity.Client.Extensibility;
using web.API;
using web.API.Extensions;
using Web.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios antes de construir la app
builder.Services.AddPresentation()
                .AddInfrastructure(builder.Configuration)
                .AddApplication();

// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://192.168.3.94:5173","http://localhost:5173") // ✅ Agrega todas las direcciones necesarias
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build(); // Ahora sí se puede construir

// Configurar el pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Middleware de errores
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandkingMiddleware>();

// Aplicar CORS antes de los controladores
app.UseCors("AllowFrontend");

app.MapControllers();
app.Run();
