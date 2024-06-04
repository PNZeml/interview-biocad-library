using Microsoft.AspNetCore.Mvc;

namespace Interview.Biocad.Library.Presentation.Api;

internal class BooksGetManyRequest {
    [FromQuery(Name = "page")]
    public int Page { get; init; } = 1;

    [FromQuery(Name = "per_page")]
    public int PerPage { get; init; } = 30;
}
