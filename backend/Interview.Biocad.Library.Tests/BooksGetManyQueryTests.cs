using System.Collections.Frozen;
using FluentAssertions;
using Interview.Biocad.Library.Application.Books;
using Interview.Biocad.Library.Configuration;
using Interview.Biocad.Library.Models.Books;

namespace Interview.Biocad.Library.Tests;

public class BooksGetManyQueryTests {
    [Fact]
    public void Query_handler_should_return_page_with_items() {
        const int totalItems = 100;

        var booksRepository = new InMemBooksRepository(new FakeBooksFetcher(totalItems));

        var sut = new BooksGetManyQueryHandler(booksRepository);

        var queryResult = sut.Handle(new BooksGetManyQuery(
            Paging: new(Page: 3, PerPage: 25),
            Filtering: new(Suggest: default, Author: default, Category: default))
        );

        queryResult.TotalItems
            .Should().Be(totalItems);

        queryResult.Items.Count
            .Should().Be(25);
    }

    private class FakeBooksFetcher(int items) : IBooksFetcher {

        public FrozenSet<Book> Fetch() {
            return Enumerable.Range(0, items)
                .Select(_ => new Book())
                .ToFrozenSet();
        }
    }
}
