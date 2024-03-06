using ChequeMicroservice.Application.Cheques.ApproveorRejectChequeRequests;
using FluentValidation;

namespace ChequeMicroservice.Application.Documents.Commands.Cheques.ApproveorRejectChequeRequests
{
    public class ApproveorRejectChequeRequestCommandValidator : AbstractValidator<ApproveorRejectChequeRequestCommand>
    {
        public ApproveorRejectChequeRequestCommandValidator()
        {
            RuleFor(x => x.ChequeId).NotEqual(0).WithMessage("Invalid cheque id");
            RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("User Id is required");
        }
    }
}
