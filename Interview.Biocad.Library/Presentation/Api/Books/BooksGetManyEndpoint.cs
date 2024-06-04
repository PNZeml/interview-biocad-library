using Interview.Biocad.Library.Application.Books;
using Interview.Biocad.Library.Models.Books;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Biocad.Library.Presentation.Api.Books;

/// <remarks>
/// Я бы предпочел использовать FastEndpoints, но поскольку задача небольшая, то решил ограничиться
/// коробочными сердствами.
/// </remarks>
internal static class BooksGetManyEndpoint {
    public static void Configure(IEndpointRouteBuilder builder) {
        builder.MapGet("/api/books", HandleAsync);
    }

    private static async Task<ICollection<Book>> HandleAsync(
        [AsParameters] BooksGetManyRequest request,
        [FromServices] BooksGetManyQueryHandler queryHandler
    ) {
        return queryHandler.Handle(ToQuery(request));
    }

    private static BooksGetManyQuery ToQuery(BooksGetManyRequest request) {
        return new(
            Paging: new(request.Page, request.PerPage),
            Filtering: new(request.Suggest, request.Author, request.Category));
    }

}
