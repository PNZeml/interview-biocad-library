using Interview.Biocad.Library.Configuration;
using Interview.Biocad.Library.Presentation;

var builder = WebApplication.CreateBuilder(args);

const string biocadEnvConfigurationPrefix = "Biocad_";

builder.Configuration
    .AddEnvironmentVariables(biocadEnvConfigurationPrefix);

builder.Services
    .AddHttpLogging(_ => { });

builder.Services
    .AddBiocadLibraryServices();

var app = builder.Build();

app.UseHttpLogging();

app.MapBiocadLibraryEndpoints();

app.Run();
