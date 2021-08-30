using System;

namespace Movies.API.Model
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }
    }
}
