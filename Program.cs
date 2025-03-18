using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework con base de datos en memoria
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseInMemoryDatabase("TaskDb"));

// Configuración de CORS para permitir solicitudes desde Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod() // Permite PUT, POST, DELETE, GET
                        .AllowAnyHeader()
                        .AllowCredentials()); // Si es necesario permitir credenciales
});

builder.Services.AddControllers();
var app = builder.Build();

app.UseCors("AllowAngular"); // Aplica la política de CORS antes de Authorization
app.UseAuthorization();
app.MapControllers();
app.Run();