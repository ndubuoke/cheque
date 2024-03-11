using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Entities;
using ChequeMicroservice.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.ChequeLeaves.Commands
{
    public class StopChequeLeafCommand : IRequest<Result>
    {
        public string LeafNumber { get; set; }
    }

    public class StopChequeLeafCommandHandler : IRequestHandler<StopChequeLeafCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public StopChequeLeafCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(StopChequeLeafCommand request, CancellationToken cancellationToken)
        {
            ChequeLeaf chequeLeaf = await _context.ChequeLeaves.FirstOrDefaultAsync(c => c.LeafNumber == request.LeafNumber, cancellationToken);
            if (chequeLeaf == null)
            {
                return Result.Failure<StopChequeLeafCommand>("No cheque record found");
            }
            chequeLeaf.ChequeLeafStatusDesc = ChequeLeafStatus.Stopped.ToString();
            chequeLeaf.LastModifiedDate = System.DateTime.Now;
            chequeLeaf.ChequeLeafStatus = ChequeLeafStatus.Stopped;

            _context.ChequeLeaves.Update(chequeLeaf);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success<StopChequeLeafCommand>("Cheque leaf stopped successfully", chequeLeaf);
        }
    }
}
