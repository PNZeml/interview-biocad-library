using Interview.Biocad.Library.Application.Books;
using Interview.Biocad.Library.Models.Books;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Interview.Biocad.Library.Configuration;

internal static class BiocadLibraryServiceCollectionEx {
    /// <summary>
    /// Add application services to the Composition Root.
    /// </summary>
    public static IServiceCollection AddBiocadLibraryServices(this IServiceCollection services) {
        services.AddOptions<BooksRepositoryOptions>()
            .BindConfiguration(BooksRepositoryOptions.Section);

        services.TryAddSingleton<IBooksFetcher, FileBooksFetcher>();

        services.TryAddSingleton<IBooksRepository, InMemBooksRepository>();

        services.TryAddTransient<BooksGetManyQueryHandler>();

        return services;
    }
}
