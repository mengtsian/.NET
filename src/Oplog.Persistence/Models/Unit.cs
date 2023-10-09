using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oplog.Persistence.Models
{
    public class Unit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public List<OperationArea> OperationAreas { get; } = new();
    }
}
