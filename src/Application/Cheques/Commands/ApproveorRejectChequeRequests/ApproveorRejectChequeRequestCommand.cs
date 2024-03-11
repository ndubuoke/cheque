using MediatR;
using Microsoft.EntityFrameworkCore;
using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using ChequeMicroservice.Domain.Entities;
using ChequeMicroservice.Application.Cheques.CreateCheques;

namespace ChequeMicroservice.Application.Cheques.ApproveorRejectChequeRequests
{
    public class ApproveorRejectChequeRequestCommand : AuthToken, IRequest<Result>
    {
        public int ChequeId { get; set; }
        public string UserId { get; set; }
        public bool IsApproved { get; set; }
    }

    public class ApproveorRejectChequeRequestCommandHandler : IRequestHandler<ApproveorRejectChequeRequestCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public ApproveorRejectChequeRequestCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(ApproveorRejectChequeRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _context.BeginTransactionAsync();
                Cheque cheque = await _context.Cheques.FirstOrDefaultAsync(a => a.Id == request.ChequeId, cancellationToken);
                if (cheque == null)
                {
                    return Result.Failure<ApproveorRejectChequeRequestCommand>("Invalid cheque id");
                }
                if (cheque.ObjectCategory == ObjectCategory.Record)
                {
                    return Result.Failure<ApproveorRejectChequeRequestCommand>("Cheque already exists");
                }
                if (request.IsApproved)
                {
                    var createChequeHandler = new CreateChequeCommandHandler(_context);
                    var chequeForCreation = new CreateChequeCommand()
                    {
                        AccessToken = request.AccessToken,
                        ChequeId = request.ChequeId,
                        UserId = request.UserId,
                    };
                    var chequeEntity = await createChequeHandler.Handle(chequeForCreation, cancellationToken);
                    if (chequeEntity.Succeeded)
                    {
                        return Result.Success<ApproveorRejectChequeRequestCommand>("Cheque request approved successfully", cheque);
                    }
                }
                cheque.ChequeStatus = ChequeStatus.Rejected;
                cheque.LastModifiedDate = DateTime.UtcNow;
                cheque.LastModifiedBy = request.UserId;
                 _context.Cheques.Update(cheque);
                await _context.SaveChangesAsync(cancellationToken);
                await _context.CommitTransactionAsync();
                return Result.Success<ApproveorRejectChequeRequestCommand>("Cheque request rejected successfully", cheque);
            }
            catch (Exception)
            {
                _context.RollbackTransaction();
                throw;
            }
        }
    }
}
