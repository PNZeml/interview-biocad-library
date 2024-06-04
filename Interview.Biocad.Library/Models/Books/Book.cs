namespace Interview.Biocad.Library.Models.Books;

/// <summary>
/// Description of the Book in the Library.
/// </summary>
internal class Book {
    public string Id { get; init; } = default!;

    public string Title { get; init; } = default!;

    public string Category { get; init; } = default!;

    public ICollection<Author> Authors { get; init; } = default!;

    public string PublicationDate { get; init; } = default!;

    public int Pages { get; init; }

    public int AgeLimit { get; init; }
}
