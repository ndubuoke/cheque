using ChequeMicroservice.Domain.Common;
using ChequeMicroservice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeMicroservice.Domain.Entities
{
    public class Cheque : BaseEntity
    {
        public DateTime IssueDate { get; set; }
        public string SeriesStartingNumber { get; set; }
        public string SeriesEndingNumber { get; set; }
        public ObjectCategory ObjectCategory { get; set; }
        public string ObjectCategoryDesc { get; set; }
        public ChequeStatus ChequeStatus { get; set; }
        public string ChequeStatusDesc { get; set; }
    }
}
