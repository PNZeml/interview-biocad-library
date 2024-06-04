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

    private static Task<BooksGetManyResponse> HandleAsync(
        [AsParameters] BooksGetManyRequest request,
        [FromServices] BooksGetManyQueryHandler queryHandler
    ) {
        var (totalItems, items) = queryHandler.Handle(ToQuery(request));

        var response = ToResponse(totalItems, items, request.Page, request.PerPage);

        return Task.FromResult(response);
    }

    private static BooksGetManyQuery ToQuery(BooksGetManyRequest request) {
        return new(
            Paging: new(request.Page, request.PerPage),
            Filtering: new(request.Suggest, request.Author, request.Category)
        );
    }

    private static BooksGetManyResponse ToResponse(
        int totalItems, ICollection<Book> items, int page, int perPage
    ) {
        return new() {
            Items = items,
            PageInfo = PageInfo.Create(totalItems, page, perPage)
        };
    }
}

internal class BooksGetManyRequest {
    [FromQuery(Name = "page")]
    public int Page { get; init; } = 1;

    [FromQuery(Name = "per_page")]
    public int PerPage { get; init; } = 30;

    [FromQuery(Name = "suggest")]
    public string? Suggest { get; init; }

    [FromQuery(Name = "author")]
    public string? Author { get; init; }

    [FromQuery(Name = "category")]
    public string? Category { get; init; }
}

internal class BooksGetManyResponse {
    public required ICollection<Book> Items { get; init; }

    public required PageInfo PageInfo { get; init; }
}

public class PageInfo {
    public int Items   { get; init; }

    public int Page    { get; init; }

    public int PerPage { get; init; }

    public int Pages   { get; init; }

    public static PageInfo Create(int items, int page, int perPage) {
        return new() {
            Items = items,
            Page = page,
            PerPage = perPage,
            Pages = (items - 1) / perPage + 1
        };
    }
}
