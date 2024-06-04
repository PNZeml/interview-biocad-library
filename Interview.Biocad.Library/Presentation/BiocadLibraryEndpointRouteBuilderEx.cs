using Interview.Biocad.Library.Presentation.Api;
using Interview.Biocad.Library.Presentation.Api.Books;

namespace Interview.Biocad.Library.Presentation;

internal static class BiocadLibraryEndpointRouteBuilderEx {
    public static void MapBiocadLibraryEndpoints(this IEndpointRouteBuilder builder) {
        BooksGetManyEndpoint.Configure(builder);
    }
}
