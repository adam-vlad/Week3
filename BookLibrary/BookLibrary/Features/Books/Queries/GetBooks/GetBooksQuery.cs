using BookLibrary.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Features.Books.Queries.GetBooks
{
    public record GetBooksQuery : IRequest<PaginatedList<BookDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, PaginatedList<BookDto>>
    {
        private readonly BookDbContext _context;

        public GetBooksQueryHandler(BookDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Books
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Year = b.Year
                })
                .AsNoTracking();

            return await PaginatedList<BookDto>.CreateAsync(
                query, 
                request.PageNumber, 
                request.PageSize);
        }
    }
}