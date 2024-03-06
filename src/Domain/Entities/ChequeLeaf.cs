using ChequeMicroservice.Domain.Common;
using ChequeMicroservice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeMicroservice.Domain.Entities
{
    public class ChequeLeaf : BaseEntity
    {
        public Guid ChequeLeafId { get; set; }
        public decimal LeafNumber { get; set; }
        public ChequeLeafStatus ChequeLeafStatus { get; set; }
    }
}
