using MediatR;
using Microsoft.EntityFrameworkCore;
using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using ChequeMicroservice.Domain.Entities;
using ChequeMicroservice.Application.ChequeLeaves.Commands;

namespace ChequeMicroservice.Application.Cheques.CreateCheques
{
    public class CreateChequeCommand : AuthToken, IRequest<Result>
    {
        public int ChequeId { get; set; }
        public string UserId { get; set; }
    }

    public class CreateChequeCommandHandler : IRequestHandler<CreateChequeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public CreateChequeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateChequeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _context.BeginTransactionAsync();
                Cheque cheque = await _context.Cheques.FirstOrDefaultAsync(a => a.Id == request.ChequeId, cancellationToken);
                if (cheque == null)
                {
                    return Result.Failure<CreateChequeCommand>("Invalid cheque id");
                }
                if (cheque.ObjectCategory == ObjectCategory.Record)
                {
                    return Result.Failure<CreateChequeCommand>("Cheque already exists");
                }

                await new CreateChequeLeavesCommandHandler(_context).Handle(new CreateChequeLeavesCommand 
                { 
                    ChequeId = cheque.Id, 
                    UserId = request.UserId,
                    Cheque = cheque
                }, cancellationToken);

                cheque.ChequeStatus = ChequeStatus.Approved;
                cheque.ObjectCategory= ObjectCategory.Record;
                cheque.LastModifiedDate = DateTime.UtcNow;
                cheque.LastModifiedBy = request.UserId;
                _context.Cheques.Update(cheque);
                await _context.SaveChangesAsync(cancellationToken);
                await _context.CommitTransactionAsync();
                return Result.Success<CreateChequeCommand>("Cheque created successfully", cheque);
            }
            catch (Exception)
            {
                _context.RollbackTransaction();
                throw;
            }
        }
    }
}
