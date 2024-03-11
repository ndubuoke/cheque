using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Entities;
using ChequeMicroservice.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.ChequeLeaves.Commands
{
    public class UpdateChequeLeafCommand : IRequest<Result>
    {
        public string LeafNumber { get; set; }
    }

    public class UpdateChequeLeafCommandHandler : IRequestHandler<UpdateChequeLeafCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public UpdateChequeLeafCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateChequeLeafCommand request, CancellationToken cancellationToken)
        {
            ChequeLeaf chequeLeaf = await _context.ChequeLeaves.FirstOrDefaultAsync(c => c.LeafNumber == request.LeafNumber, cancellationToken);
            if (chequeLeaf == null)
            {
                return Result.Failure("No cheque record found");
            }
            chequeLeaf.LastModifiedDate = DateTime.Now;
            chequeLeaf.ChequeLeafStatus = ChequeLeafStatus.Used;
            chequeLeaf.ChequeLeafStatusDesc = ChequeLeafStatus.Used.ToString();
            _context.ChequeLeaves.Update(chequeLeaf);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Cheque leaf updated successfully", chequeLeaf);
        }
    }
}
