
using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Common.Models;
using ChequeMicroservice.Domain.Entities;
using ChequeMicroservice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ChequeMicroservice.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private IDbContextTransaction _currentTransaction;
        private IConfiguration _configuration;

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("PostgreSQLConnection");

                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<ChequeLeaf> ChequeLeaves { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            List<AuditEntry> auditEntries = new List<AuditEntry>();
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditTrail || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                AuditEntry auditEntry = new AuditEntry(entry);
                auditEntry.EntityName = entry.Entity.GetType().Name;
                auditEntries.Add(auditEntry);
                foreach (PropertyEntry property in entry.Properties)
                {
                    string entityId = property.EntityEntry.Property("EntityId").CurrentValue.ToString();
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditAction = AuditAction.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            auditEntry.EntityId = entityId;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditAction = AuditAction.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.EntityId = entityId;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditAction = AuditAction.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.EntityId = entityId;
                            }
                            break;
                    }
                }
            }
            foreach (AuditEntry auditEntry in auditEntries)
            {
                AuditTrails.Add(auditEntry.ToAudit());
            }
        }
        #region RawSql

        public int ExecuteSqlRaw(string sql, params object[] parameters)
        {
            return base.Database.ExecuteSqlRaw(sql, parameters);
        }

        public async Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
        {
            return await base.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<int> ExecuteSqlRawAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            return await base.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
        }

        public int ExecuteSqlInterpolated(FormattableString sql)
        {
            return base.Database.ExecuteSqlInterpolated(sql);
        }

        public async Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql, CancellationToken cancellationToken = default)
        {
            return await base.Database.ExecuteSqlInterpolatedAsync(sql, cancellationToken);
        }

        #endregion


        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

    }
}

