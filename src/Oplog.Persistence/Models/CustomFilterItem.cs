namespace Oplog.Persistence.Models
{
    public class CustomFilterItem
    {
        public int Id { get; set; }
        public int CustomFilterId { get; set; }
        public int FilterId { get; set; }
        public int? CategoryId { get; set; }
        public CustomFilter CustomFilter { get; set; }
    }
}
