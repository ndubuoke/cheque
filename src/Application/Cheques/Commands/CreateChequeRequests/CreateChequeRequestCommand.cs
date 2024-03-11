using MediatR;
using Microsoft.EntityFrameworkCore;
using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using ChequeMicroservice.Domain.Entities;

namespace ChequeMicroservice.Application.Cheques.CreateCheques
{
    public class CreateChequeRequestCommand : AuthToken, IRequest<Result>
    {
        public DateTime IssueDate { get; set; }
        public string SeriesStartingNumber { get; set; }
        public string SeriesEndingNumber { get; set; }
        public int NumberOfChequeLeaf { get; set; }
        public string UserId { get; set; }
    }

    public class CreateChequeRequestCommandHandler : IRequestHandler<CreateChequeRequestCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public CreateChequeRequestCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateChequeRequestCommand request, CancellationToken cancellationToken)
        {
            Cheque existingCheque = await _context.Cheques.FirstOrDefaultAsync(a => a.SeriesStartingNumber == request.SeriesStartingNumber && a.SeriesEndingNumber == request.SeriesEndingNumber, cancellationToken);
            if (existingCheque != null)
            {
                return Result.Failure("An active cheque already exist");
            }
            Cheque newCheque = new Cheque
            {
                ChequeStatus = ChequeStatus.Initiated,
                ChequeStatusDesc = ChequeStatus.Initiated.ToString(),
                ObjectCategory = ObjectCategory.Request,
                ObjectCategoryDesc = ObjectCategory.Request.ToString(),
                CreatedDate = DateTime.UtcNow,
                CreatedBy = request.UserId,
                SeriesEndingNumber = request.SeriesEndingNumber,
                SeriesStartingNumber = request.SeriesStartingNumber,
                IssueDate = request.IssueDate,
                NumberOfChequeLeaf = request.NumberOfChequeLeaf,
                Status = Status.Active,
                StatusDesc = Status.Active.ToString().ToString(),
                EntityId = "",//Get this from user id, we need a method to fetch user details and permissions
                BranchId = "",//Get this from user id, we need a method to fetch user details and permissions
            };
            await _context.Cheques.AddAsync(newCheque, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success("Cheque created successfully", newCheque);
        }
    }
}
