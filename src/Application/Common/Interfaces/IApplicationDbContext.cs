using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ChequeMicroservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
       
        DbSet<AuditTrail> AuditTrails { get; set; }
        DbSet<Domain.Entities.Cheque> Cheques { get; set; }
        DbSet<ChequeLeaf> ChequeLeaves { get; set; }

        Task BeginTransactionAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync();
        void RollbackTransaction();
    

    }
}
