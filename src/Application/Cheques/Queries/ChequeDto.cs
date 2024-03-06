using AutoMapper;
using ChequeMicroservice.Application.Common.Mappings;
using ChequeMicroservice.Domain.Entities;
using ChequeMicroservice.Domain.Enums;
using System;

namespace ChequeMicroservice.Application.Documents.Queries
{
    public class ChequeDto : IMapFrom<Cheque>
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public string LastModifiedBy { get; set; }
		public DateTime? LastModifiedDate { get; set; }
		public Status Status { get; set; }
		public string StatusDesc { get; set; }
        public Guid ChequeId { get; set; }
        public DateTime IssueDate { get; set; }
        public string SeriesStartingNumber { get; set; }
        public string SeriesEndingNumber { get; set; }
        public ObjectCategory ObjectCategory { get; set; }
        public ChequeStatus ChequeStatus { get; set; }
        public string ChequeStatusDesc { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Cheque, ChequeDto>();
            profile.CreateMap<ChequeDto, Cheque>();
        }
    }
}
