using ChequeMicroservice.Application.Cheques;
using ChequeMicroservice.Application.Cheques.CreateCheques;
using FluentValidation;

namespace ChequeMicroservice.Application.Documents.Commands.Cheques.CreateCheques
{
    public class CreateChequeRequestCommandValidator : AbstractValidator<CreateChequeRequestCommand>
    {
        public CreateChequeRequestCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("User Id is required");
        }
    }
}
