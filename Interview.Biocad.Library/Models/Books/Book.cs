namespace Interview.Biocad.Library.Models.Books;

/// <summary>
/// Description of the Book in the Library.
/// </summary>
public class Book {
    public string Id { get; init; }

    public string Title { get; init; }

    public string Category { get; init; }

    public ICollection<Author> Authors { get; init; }

    public string PublicationDate { get; init; }

    public int Pages { get; init; }

    public int AgeLimit { get; init; }
}
