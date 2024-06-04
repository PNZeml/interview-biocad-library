using Interview.Biocad.Library.Configuration;
using Interview.Biocad.Library.Presentation;

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

builder.Services
    .AddHttpLogging(_ => { });

builder.Services
    .AddBiocadLibraryServices();

var app = builder.Build();

app.UseCors();
app.UseHttpLogging();

app.MapBiocadLibraryEndpoints();

app.Run();
