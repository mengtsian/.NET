using System;

namespace Oplog.Persistence.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? LastChangeTime { get; set; }
        public bool? IsRequired { get; set; }
    }
}
