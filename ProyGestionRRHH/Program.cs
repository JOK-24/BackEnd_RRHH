using Microsoft.EntityFrameworkCore;
using ProyGestionRRHH.Models;

var builder = WebApplication.CreateBuilder(args);

// Recuperando cadena de conexión
var conexion = builder.Configuration.GetConnectionString("cn1");
builder.Services.AddDbContext<GestionRrhhContext>(opt => opt.UseSqlServer(conexion));

// Añadir servicios CORS para React
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5174") // URL donde corre React
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS
app.UseCors("ReactCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
