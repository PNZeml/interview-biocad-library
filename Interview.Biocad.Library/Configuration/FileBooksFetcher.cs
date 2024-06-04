using System.Collections.Frozen;
using System.Text.Json;
using Interview.Biocad.Library.Models.Books;
using Microsoft.Extensions.Options;

namespace Interview.Biocad.Library.Configuration;

internal class FileBooksFetcher : IBooksFetcher {
    private readonly string booksFilePath;

    public FileBooksFetcher(IOptions<BooksRepositoryOptions> options) {
        booksFilePath = options.Value.Path;
    }

    public FrozenSet<Book> Load() {
        if (Path.Exists(booksFilePath) == false) {
            throw new Exception("Provided Books file does not exist");
        }

        using var booksFileStream = File.OpenRead(booksFilePath);

        var books = JsonSerializer.Deserialize<ICollection<Book>>(booksFileStream, options: new() {
            PropertyNameCaseInsensitive = true,
        });

        if (books is null) {
            throw new Exception("Could not deserialize underlying Books store");
        }

        return books.ToFrozenSet();
    }
}
