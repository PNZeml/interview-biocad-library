using System.Collections.Frozen;
using Interview.Biocad.Library.Models.Books;

namespace Interview.Biocad.Library.Configuration;

internal interface IBooksFetcher {
    public FrozenSet<Book> Fetch();
}
