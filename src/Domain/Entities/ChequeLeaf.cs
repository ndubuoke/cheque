using ChequeMicroservice.Domain.Common;
using ChequeMicroservice.Domain.Enums;
using System;

namespace ChequeMicroservice.Domain.Entities
{
    public class ChequeLeaf : BaseEntity
    {
        public int ChequeId { get; set; }
        public Guid ChequeLeafId { get; set; } = Guid.NewGuid();
        public decimal LeafNumber { get; set; }
        public ChequeLeafStatus ChequeLeafStatus { get; set; }
    }
}
