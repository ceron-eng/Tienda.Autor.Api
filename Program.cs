using Microsoft.Extensions.Options;
using Tienda.Autor.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

// Configurar servicios
builder.Services.Configure<ImageDatabaseSettings>(
    builder.Configuration.GetSection(nameof(ImageDatabaseSettings)));

builder.Services.AddSingleton<ImageDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<ImageDatabaseSettings>>().Value);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddSingleton<ImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ImageGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.UseCors("AllowAllOrigins");
app.Run();
