using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

namespace ChequeMicroservice.Application.ChequeLeaves.Queries
{
    public class ConfirmChequeLeafQuery : IRequest<Result>
    {
        public string LeafNumber { get; set; }
    }

    public class ConfirmChequeLeafQueryHandler : IRequestHandler<ConfirmChequeLeafQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        public ConfirmChequeLeafQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(ConfirmChequeLeafQuery request, CancellationToken cancellationToken)
        {
            ChequeLeaf chequeLeaf = await _context.ChequeLeaves.FirstOrDefaultAsync(c => c.LeafNumber == request.LeafNumber);
            if (chequeLeaf == null)
            {
                return Result.Failure<GetChequeLeavesQuery>("No check leaf record found");
            }
            return Result.Success<GetChequeLeavesQuery>("Cheque leaf confirmed successfully", chequeLeaf);
        }
    }
}
