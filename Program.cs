// Program.cs

using ClientesApi.Middleware; // This line is already present and correct.
using ClientesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Añadir servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- CAMBIO CLAVE ---
// En lugar de AddDbContext, registramos nuestro repositorio en memoria como un Singleton.
// Singleton significa que solo habrá una instancia de esta clase durante toda la vida de la aplicación.
builder.Services.AddSingleton<ClienteRepository>();


var app = builder.Build();

// 2. Configurar el pipeline de peticiones HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 3. Registrar nuestro Middleware de API KEY.
app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();