using Interview.Biocad.Library.Presentation.Api;

namespace Interview.Biocad.Library.Presentation;

internal static class PresentationEndpointRouteBuilderEx {
    public static void MapBiocadLibraryEndpoints(this IEndpointRouteBuilder builder) {
        BooksGetManyEndpoint.Configure(builder);
    }
}
