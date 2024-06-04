using Interview.Biocad.Library.Models.Books;

namespace Interview.Biocad.Library.Application.Books;

/// <summary>
/// Query to fetch some Books optionally using filtering parameters.
/// </summary>
/// <param name="Paging">
/// Pagination parameters.
/// </param>
/// <param name="Filtering">
/// Filtering parameters.
/// </param>
internal record BooksGetManyQuery(BooksPagingParams Paging, BooksFilteringParams Filtering);

internal record BooksPagingParams(int Page, int PerPage);

internal record BooksFilteringParams(string? Suggest, string? Author, string? Category);

internal class BooksGetManyQueryHandler(IBooksRepository repository) {
    public (int TotalItems, ICollection<Book> Items) Handle(BooksGetManyQuery query) {
        var filteredQuery = repository.Query()
            .WithFiltering(query.Filtering)
            .ToList();

        // Note: В теоррии, должно быть обращение к внешнему сервису (например, СУБД), для подсчета
        // общего кол-ва фильтрованных записей.
        var totalItems = filteredQuery.Count;

        var items = filteredQuery.WithPaging(query.Paging).ToList();

        return (totalItems, items);
    }
}
