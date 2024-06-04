using Interview.Biocad.Library.Configuration;
using Interview.Biocad.Library.Presentation;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

const string biocadEnvConfigurationPrefix = "Biocad_";

builder.Configuration
    .AddEnvironmentVariables(biocadEnvConfigurationPrefix);

builder.Services
    .AddCors(options => {
        options.AddDefaultPolicy(policy => policy
            .AllowAnyMethod()
            .AllowAnyOrigin());
    });

// В задании сказано, что нужно "протоколировать запросы и ответы к сервисы".
// Предполагаю, что имелось в виду логирование "коробычными" средствами.
builder.Services
    .AddHttpLogging(options => {
        options.LoggingFields = HttpLoggingFields.All;
    });

builder.Services
    .AddBiocadLibraryServices();

var app = builder.Build();

app.UseCors();
app.UseHttpLogging();

app.MapBiocadLibraryEndpoints();

app.Run();
