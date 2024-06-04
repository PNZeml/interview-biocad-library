using Interview.Biocad.Library.Models.Books;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Biocad.Library.Presentation.Api;

/// <remarks>
/// Я бы предпочел использовать FastEndpoints, но поскольку задача небольшая, то решил ограничиться
/// коробочными сердствами.
/// </remarks>
internal static class BooksGetManyEndpoint {
    /// <summary>
    /// Map endpoints for "Books" resource
    /// </summary>
    public static void Configure(IEndpointRouteBuilder builder) {
        builder.MapGet("/api/books", HandleAsync);
    }

    private static async Task<List<Book>> HandleAsync(
        [AsParameters] BooksGetManyRequest req,
        [FromServices] IBooksRepository repository
    ) {
        var query = repository.Query();

        var itemsToSkip = (req.Page - 1) * req.PerPage;

        return query.Skip(itemsToSkip)
            .Take(req.PerPage)
            .ToList();
    }
}
