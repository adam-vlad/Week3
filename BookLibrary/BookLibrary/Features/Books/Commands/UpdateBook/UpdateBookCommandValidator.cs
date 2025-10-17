using FluentValidation;

namespace BookLibrary.Features.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(v => v.Author)
                .NotEmpty().WithMessage("Author is required.")
                .MaximumLength(100).WithMessage("Author must not exceed 100 characters.");

            RuleFor(v => v.Year)
                .NotEmpty().WithMessage("Year is required.")
                .GreaterThan(0).WithMessage("Year must be greater than 0.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage($"Year must be less than or equal to current year ({DateTime.Now.Year}).");
        }
    }
}