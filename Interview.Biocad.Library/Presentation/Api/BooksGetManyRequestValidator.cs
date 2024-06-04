using FluentValidation;

namespace Interview.Biocad.Library.Presentation.Api;

internal class BooksGetManyRequestValidator : AbstractValidator<BooksGetManyRequest> {
    public BooksGetManyRequestValidator() {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PerPage)
            .InclusiveBetween(5, 100);
    }
}
