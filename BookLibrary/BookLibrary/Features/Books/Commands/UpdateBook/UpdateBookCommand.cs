using BookLibrary.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Features.Books.Commands.UpdateBook
{
    public record UpdateBookCommand : IRequest<bool>
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Author { get; init; } = string.Empty;
        public int Year { get; init; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly BookDbContext _context;

        public UpdateBookCommandHandler(BookDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(new object[] { request.Id }, cancellationToken);

            if (book == null)
            {
                return false;
            }

            book.Title = request.Title;
            book.Author = request.Author;
            book.Year = request.Year;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}