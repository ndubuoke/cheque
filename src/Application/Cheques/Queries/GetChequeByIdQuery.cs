using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.Cheques.Queries
{
    public class GetChequeByIdQuery : IRequest<Result>
    {
        public int ChequeId { get; set; }
    }

    public class GetChequeByIdQueryHandler : IRequestHandler<GetChequeByIdQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        public GetChequeByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(GetChequeByIdQuery request, CancellationToken cancellationToken)
        {
            Cheque cheque = await _context.Cheques.FirstOrDefaultAsync(c => c.Id == request.ChequeId);
            if (cheque == null)
            {
                return Result.Failure<GetChequeByIdQuery>("No cheque record found");
            }
            return Result.Success<GetChequeByIdQuery>("Cheque retrieved successfully", cheque);
        }
    }
}
