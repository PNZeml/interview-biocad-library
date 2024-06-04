using Interview.Biocad.Library.Models.Books;

namespace Interview.Biocad.Library.Application.Books;

internal static class BooksEnumerableEx {
    public static IEnumerable<Book> WithFiltering(this IEnumerable<Book> query, BooksFilteringParams @params) {
        if (string.IsNullOrEmpty(@params.Suggest) == false) {
            query = query.Where(x => x.Title
                .Contains(@params.Suggest, StringComparison.InvariantCultureIgnoreCase));
        }

        if (string.IsNullOrEmpty(@params.Author) == false) {
            query = query.Where(x => x.Authors
                .Any(y => y.Name
                    .Contains(@params.Author, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (string.IsNullOrEmpty(@params.Category) == false) {
            query = query.Where(x => x.Category
                .Contains(@params.Category, StringComparison.InvariantCultureIgnoreCase));
        }

        return query;
    }

    public static IEnumerable<Book> WithPaging(this IEnumerable<Book> query, BooksPagingParams @params) {
        var itemsToSkip = (@params.Page - 1) * @params.PerPage;

        return query.Skip(itemsToSkip)
            .Take(@params.PerPage);
    }
}
