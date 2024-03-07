using FluentValidation;

namespace ChequeMicroservice.Application.ChequeLeaves.Commands
{
    public class CreateChequeLeavesCommandValidator : AbstractValidator<CreateChequeLeavesCommand>
    {
        public CreateChequeLeavesCommandValidator()
        {
            RuleFor(x => x.ChequeId).NotEqual(0).WithMessage("Invalid cheque id");
            RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("User Id is required");
        }
    }
}
