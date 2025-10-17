using BookLibrary.Domain;
using BookLibrary.Features.Books.Commands.CreateBook;
using BookLibrary.Features.Books.Commands.DeleteBook;
using BookLibrary.Features.Books.Commands.UpdateBook;
using BookLibrary.Features.Books.Queries.GetBookById;
using BookLibrary.Features.Books.Queries.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<BookDto>>> GetBooks([FromQuery] GetBooksQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBook(CreateBookCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBook), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand(id));

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}