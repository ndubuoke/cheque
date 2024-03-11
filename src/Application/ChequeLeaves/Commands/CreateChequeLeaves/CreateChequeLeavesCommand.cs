using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Entities;
using ChequeMicroservice.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.ChequeLeaves.Commands
{
    public class CreateChequeLeavesCommand : IRequest<Result>
    {
        public int ChequeId { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public Cheque Cheque { get; set; }
    }

    public class CreateChequeLeavesCommandHandler : IRequestHandler<CreateChequeLeavesCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public CreateChequeLeavesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateChequeLeavesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _context.BeginTransactionAsync();
                Cheque cheque = request.Cheque != null ? request.Cheque : await _context.Cheques.FirstOrDefaultAsync(a => a.Id == request.ChequeId, cancellationToken);

                List<ChequeLeaf> chequeLeaves = new List<ChequeLeaf>();

                bool isStartingSeriesNumber = long.TryParse(cheque.SeriesStartingNumber, out long chequeStartingSeriesValue);
                bool isEndingSeriesNumber = long.TryParse(cheque.SeriesEndingNumber, out long chequeEndingSeriesValue);

                if (!isEndingSeriesNumber || !isStartingSeriesNumber)
                {
                    return Result.Failure<CreateChequeLeavesCommand>("An error occured while trying to create cheque leaves");
                }
                if (chequeStartingSeriesValue > chequeEndingSeriesValue)
                {
                    return Result.Failure<CreateChequeLeavesCommand>("An error occured while trying to create cheque leaves. Series not in appropriate order");
                }

                for (long leafNumber = chequeStartingSeriesValue; leafNumber <= chequeEndingSeriesValue; leafNumber++)
                {
                    chequeLeaves.Add(new ChequeLeaf
                    {
                        LeafNumber = leafNumber.ToString(),
                        ChequeId = cheque.Id,
                        ChequeLeafStatus = ChequeLeafStatus.Available,
                        Status = Status.Active,
                        StatusDesc = Status.Active.ToString(),
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = request.UserId
                    });
                }

                await _context.ChequeLeaves.AddRangeAsync(chequeLeaves, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await _context.CommitTransactionAsync();
                return Result.Success<CreateChequeLeavesCommand>("Cheque leaves created successfully", chequeLeaves);
            }
            catch (Exception)
            {
                _context.RollbackTransaction();
                throw;
            }
        }
    }
}
