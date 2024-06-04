using Interview.Biocad.Library.Configuration;
using Interview.Biocad.Library.Presentation;

var builder = WebApplication.CreateBuilder(args);

const string biocadEnvConfigurationPrefix = "Biocad_";

builder.Configuration
    .AddEnvironmentVariables(biocadEnvConfigurationPrefix);

builder.Services
    .AddBiocadLibraryServices();

var app = builder.Build();

app.MapBiocadLibraryEndpoints();

app.Run();
