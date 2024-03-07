﻿using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.ChequeLeaves.Queries
{
    public class GetChequeLeavesQuery : IRequest<Result>
    {
        public int ChequeId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class GetChequeLeavesQueryHandler : IRequestHandler<GetChequeLeavesQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        public GetChequeLeavesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(GetChequeLeavesQuery request, CancellationToken cancellationToken)
        {
            List<ChequeLeaf> chequeLeaves = await _context.ChequeLeaves.Where(c => c.ChequeId == request.ChequeId).ToListAsync();
            if (!chequeLeaves.Any())
            {
                return Result.Failure<GetChequeLeavesQuery>("No cheque leaves record found");
            }
            if (request.Skip > 0 || request.Take > 0)
            {
                return Result.Success<GetChequeLeavesQuery>("Cheque leaves retrieved successfully", chequeLeaves.Skip(request.Skip).Take(request.Take).ToList());
            }
            return Result.Success<GetChequeLeavesQuery>("Cheque leaves retrieved successfully", chequeLeaves);
        }
    }
}
