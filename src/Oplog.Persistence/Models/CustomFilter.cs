using System.Collections.Generic;

namespace Oplog.Persistence.Models
{
    public class CustomFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CustomFilterItem> CustomFilterItems { get; set; }
        public string CreatedBy { get; set; }
        public bool IsGlobalFilter { get; set; }
        public string SearchText { get; set; }
    }
}
