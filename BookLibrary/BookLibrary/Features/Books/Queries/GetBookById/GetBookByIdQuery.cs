using BookLibrary.Data;
using BookLibrary.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Features.Books.Queries.GetBookById
{
    public record GetBookByIdQuery(int Id) : IRequest<Book?>;

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book?>
    {
        private readonly BookDbContext _context;

        public GetBookByIdQueryHandler(BookDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Books.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}