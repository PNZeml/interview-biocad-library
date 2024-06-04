namespace Interview.Biocad.Library.Models.Books;

public interface IBooksRepository {
    /// <summary>
    /// Query to fetch Books from external source.
    /// </summary>
    IEnumerable<Book> Query();
}
