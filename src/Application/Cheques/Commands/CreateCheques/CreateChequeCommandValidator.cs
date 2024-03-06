using ChequeMicroservice.Application.Cheques;
using ChequeMicroservice.Application.Cheques.CreateCheques;
using FluentValidation;

namespace ChequeMicroservice.Application.Documents.Commands.Cheques.CreateCheques
{
    public class CreateChequeCommandValidator : AbstractValidator<CreateChequeCommand>
    {
        public CreateChequeCommandValidator()
        {
            RuleFor(x => x.ChequeId).NotEqual(0).WithMessage("Invalid cheque id");
            RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("User Id is required");
        }
    }
}
