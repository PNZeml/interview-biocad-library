using System.Collections.Frozen;
using System.Text.Json;
using Interview.Biocad.Library.Models.Books;
using Microsoft.Extensions.Options;

namespace Interview.Biocad.Library.Configuration;

public class FileBooksRepository : IBooksRepository {
    private readonly Lazy<FrozenSet<Book>> underlyingStore;

    public FileBooksRepository(IOptions<FileBooksRepositoryOptions> options) {
        underlyingStore = new Lazy<FrozenSet<Book>>(() => {
            using var booksFileStream = File.OpenRead(options.Value.Path);

            var books = JsonSerializer.Deserialize<ICollection<Book>>(
                booksFileStream,
                new JsonSerializerOptions(JsonSerializerDefaults.General) {
                    PropertyNameCaseInsensitive = true,
                }
            );

            if (books is null) {
                throw new Exception("Could not deserialize underlying Books store");
            }

            return books.ToFrozenSet();
        }, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    public IEnumerable<Book> Query() {
        return underlyingStore.Value.AsEnumerable();
    }
}
