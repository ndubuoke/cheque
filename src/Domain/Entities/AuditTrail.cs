
using ChequeMicroservice.Domain.Common;
using ChequeMicroservice.Domain.Enums;

namespace ChequeMicroservice.Domain.Entities
{
    public class AuditTrail : BaseEntity
    {
        public AuditAction AuditAction { get; set; }
        public string AuditActionDesc { get; set; }
        public string EntityName { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string MicroserviceName { get; set; }
    }
}
