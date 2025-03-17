using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework con base de datos en memoria
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseInMemoryDatabase("TaskDb"));

// Habilitar CORS y permitir todos los métodos (GET, POST, PUT, DELETE)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod() // ✅ Asegura que PUT y DELETE sean permitidos
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
var app = builder.Build();

app.UseCors("AllowAll"); // Aplica la política de CORS
app.UseAuthorization();
app.MapControllers();

app.Run();