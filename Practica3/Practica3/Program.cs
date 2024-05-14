using PatientEntities1.Managers;
using Serilog;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();
builder.Services.AddTransient<PatientManager1>(); // Registrar PatientManager1
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de Serilog
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile(
        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json"
    )
    .Build();

if (builder.Environment.EnvironmentName == "QA")
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.File(builder.Configuration.GetSection("Paths").GetSection("FileLocation").Value, rollingInterval: RollingInterval.Day)
        .CreateLogger();
    Log.Information("Initializing the server environment QA");
}
else
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File(builder.Configuration.GetSection("Paths").GetSection("FileLocation").Value, rollingInterval: RollingInterval.Day)
        .CreateLogger();
    Log.Information("Initializing the server not environment QA");
}

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
