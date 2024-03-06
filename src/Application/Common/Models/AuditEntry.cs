using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using ChequeMicroservice.Domain.Entities;
using ChequeMicroservice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeMicroservice.Application.Common.Models
{
    public class AuditEntry 
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string CreatedByEmail { get; set; }
        public string EntityName { get; set; }
        public string MicroserviceName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public AuditAction AuditAction { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();
        public AuditTrail ToAudit()
        {
            AuditTrail audit = new AuditTrail();
            audit.UserId = UserId;
            audit.AuditAction = AuditAction;
            audit.AuditActionDesc = AuditAction.ToString();
            audit.EntityName = EntityName;
            audit.CreatedDate = DateTime.Now;
            audit.CreatedByEmail = CreatedByEmail;
            audit.Status = Status.Active;
            audit.EntityId = EntityId;
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            audit.MicroserviceName = "Cheque Microservice";
            return audit;
        }
    }
}
