using ChequeMicroservice.Domain.Common;
using ChequeMicroservice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeMicroservice.Domain.Entities
{
    public class ChequeLeaf : BaseEntity
    {
        public int ChequeId { get; set; }
        public Guid ChequeLeafId { get; set; }
        public string LeafNumber { get; set; }
        public ChequeLeafStatus ChequeLeafStatus { get; set; }
        public string ChequeLeafStatusDesc { get; set; }
    }
}
