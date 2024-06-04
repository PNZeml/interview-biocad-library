using System.Collections.Frozen;
using Interview.Biocad.Library.Models.Books;

namespace Interview.Biocad.Library.Configuration;

internal class InMemBooksRepository : IBooksRepository {
    private readonly Lazy<FrozenSet<Book>> underlyingStore;

    public InMemBooksRepository(IBooksFetcher fetcher) {
        underlyingStore = new(fetcher.Fetch, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    public IEnumerable<Book> Query() {
        return underlyingStore.Value.AsEnumerable();
    }
}
