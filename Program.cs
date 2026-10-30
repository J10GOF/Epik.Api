
using Epik.Api.Data;
using Microsoft.EntityFrameworkCore;
using Epik.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Obtener cadena de conexión
var connectionString = builder.Configuration
    .GetConnectionString("Postgres")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión PostgreSQL."
    );

// Registrar Entity Framework y PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
);
// Registrar servicios
builder.Services.AddScoped<IPersonaService, PersonaService>();

// Registrar controladores
builder.Services.AddControllers();

// Registrar OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Configuración de OpenAPI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Middleware
app.UseHttpsRedirection();

// Mapear controladores
app.MapControllers();

app.Run();