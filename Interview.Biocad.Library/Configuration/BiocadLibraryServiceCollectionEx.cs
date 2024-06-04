using Interview.Biocad.Library.Models.Books;

namespace Interview.Biocad.Library.Configuration;

internal static class BiocadLibraryServiceCollectionEx {
    /// <summary>
    /// Add application services to the Composition Root.
    /// </summary>
    public static IServiceCollection AddBiocadLibraryServices(this IServiceCollection services) {
        services.AddOptions<FileBooksRepositoryOptions>()
            .BindConfiguration(FileBooksRepositoryOptions.Section);

        services.AddSingleton<IBooksRepository, FileBooksRepository>();

        return services;
    }
}
