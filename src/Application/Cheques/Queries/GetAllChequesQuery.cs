using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.Cheques.Queries
{
    public class GetAllChequesQuery : IRequest<Result>
    {
    }

    public class GetAllChequesQueryHandler : IRequestHandler<GetAllChequesQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        public GetAllChequesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(GetAllChequesQuery request, CancellationToken cancellationToken)
        {
            List<Cheque> cheques = await _context.Cheques.ToListAsync(cancellationToken);
            if (cheques == null || cheques.Count == 0)
            {
                return Result.Failure("No cheque records found");
            }
            return Result.Success("Cheques retrieved successfully", cheques);
        }
    }
}
