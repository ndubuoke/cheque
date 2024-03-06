using ChequeMicroservice.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChequeMicroservice.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid ObjectGuId { get; set; } = Guid.NewGuid();
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedByName { get; set; }
        public Status Status { get; set; }
        public string StatusDesc { get; set; }
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string BranchId { get; set; }

    }


}
