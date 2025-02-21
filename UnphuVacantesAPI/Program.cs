using Microsoft.EntityFrameworkCore;
using UnphuVacantesAPI.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 💡 Agregar esto para evitar el error
builder.Services.AddAuthorization();

// Cargar las variables de entorno desde el archivo .env
Env.Load();

// Configurar la cadena de conexión usando las variables de entorno
var dbId = Environment.GetEnvironmentVariable("DB_ID");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Configura el DbContext usando las variables de entorno en la cadena de conexión
builder.Services.AddDbContext<UnphuVacantesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    .Replace("${DB_ID}", dbId)
    .Replace("${DB_PASSWORD}", dbPassword))
);

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Dirección de tu frontend Angular
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configura el middleware para usar CORS
app.UseCors("AllowAngularApp");

// Configurar Swagger solo en Desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger.json", "Unphu Vacantes API v1");
    });
}

app.UseHttpsRedirection();

// 🛠️ Esto depende de AddAuthorization()
app.UseAuthorization();

// Configura el middleware para servir archivos estáticos
app.UseStaticFiles();

app.MapControllers();

app.Run();
