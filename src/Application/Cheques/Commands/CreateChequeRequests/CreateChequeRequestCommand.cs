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
            try
            {
                Cheque existingCheck = await _context.Cheques.FirstOrDefaultAsync(c => c.Status == Status.Active);
                if (existingCheck != null)
                {
                    return Result.Failure<CreateChequeRequestCommand>("An active check already exist");
                }
                await _context.BeginTransactionAsync();
                Cheque cheque = await _context.Cheques.FirstOrDefaultAsync(a => a.SeriesStartingNumber == request.SeriesStartingNumber || a.SeriesEndingNumber == request.SeriesEndingNumber);
                if (cheque != null)
                {
                    if (cheque.SeriesStartingNumber == request.SeriesStartingNumber)
                    {
                        return Result.Failure<CreateChequeRequestCommand>("Cheque already exists with this series start date");
                    }
                    if (cheque.SeriesEndingNumber == request.SeriesEndingNumber)
                    {
                        return Result.Failure<CreateChequeRequestCommand>("Cheque already exists with this series end date");
                    }
                }
                if (cheque != null && cheque.ObjectCategory == ObjectCategory.Record)
                {
                    return Result.Failure<CreateChequeRequestCommand>("Cheque already exists");
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
                    BranchId = "" ,//Get this from user id, we need a method to fetch user details and permissions
                };
                await _context.Cheques.AddAsync(newCheque);
                await _context.SaveChangesAsync(cancellationToken);
                await _context.CommitTransactionAsync();
                return Result.Success<CreateChequeRequestCommand>("Cheque created successfully", cheque);
            }
            catch (Exception)
            {
                _context.RollbackTransaction();
                throw;
            }
        }
    }
}
