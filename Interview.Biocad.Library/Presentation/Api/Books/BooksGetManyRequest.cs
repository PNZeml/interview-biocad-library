using Microsoft.AspNetCore.Mvc;

namespace Interview.Biocad.Library.Presentation.Api.Books;

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
