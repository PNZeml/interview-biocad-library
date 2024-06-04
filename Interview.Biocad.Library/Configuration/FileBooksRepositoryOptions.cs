namespace Interview.Biocad.Library.Configuration;

public class FileBooksRepositoryOptions {
    public const string Section = "Library:Books";

    /// <summary>
    /// The path to the file with a book list.
    /// </summary>
    public string Path { get; init; } = default!;
}
