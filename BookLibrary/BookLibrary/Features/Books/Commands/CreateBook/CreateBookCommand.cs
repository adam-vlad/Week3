using BookLibrary.Data;
using BookLibrary.Domain;
using MediatR;

namespace BookLibrary.Features.Books.Commands.CreateBook
{
    public record CreateBookCommand : IRequest<int>
    {
        public string Title { get; init; } = string.Empty;
        public string Author { get; init; } = string.Empty;
        public int Year { get; init; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly BookDbContext _context;

        public CreateBookCommandHandler(BookDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Year = request.Year
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}