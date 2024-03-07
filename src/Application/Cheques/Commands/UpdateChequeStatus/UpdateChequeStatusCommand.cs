using ChequeMicroservice.Application.Common.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.Cheques.Commands
{
    public class UpdateChequeStatusCommand : IRequest<Result>
    {
    }

    public class UpdateChequeStatusCommandHandler : IRequestHandler<UpdateChequeStatusCommand, Result>
    {
        public Task<Result> Handle(UpdateChequeStatusCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
