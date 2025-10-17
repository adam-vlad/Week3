using BookLibrary.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Features.Books.Commands.DeleteBook
{
    public record DeleteBookCommand(int Id) : IRequest<bool>;

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly BookDbContext _context;

        public DeleteBookCommandHandler(BookDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(new object[] { request.Id }, cancellationToken);

            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}