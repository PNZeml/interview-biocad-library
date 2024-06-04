using Interview.Biocad.Library.Models.Books;

namespace Interview.Biocad.Library.Application.Books;

internal record BooksGetManyQuery(BooksPagingParams Paging, BooksFilteringParams Filtering);

internal record BooksPagingParams(int Page, int PerPage);

internal record BooksFilteringParams(string? Suggest, string? Author, string? Category);

internal class BooksGetManyQueryHandler(IBooksRepository repository) {
    public ICollection<Book> Handle(BooksGetManyQuery query) {
        return repository.Query()
            .WithFiltering(query.Filtering)
            .WithPaging(query.Paging)
            .ToList();
    }
}
