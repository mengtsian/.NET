using System;

namespace Oplog.Persistence.Models
{
    public class ConfiguredType
    {
        public int Id { get; set; }
        public int? LastChangeUserId { get; set; }
        public DateTime? LastChangeTime { get; set; }
        public DateTime? StartLife { get; set; }
        public DateTime? EndLife { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Priority { get; set; }
        public int? UomTypeId { get; set; }
        public int? DefaultUomTypeId { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsDuplicate { get; set; }
        public bool? IsActive { get; set; }
    }
}
