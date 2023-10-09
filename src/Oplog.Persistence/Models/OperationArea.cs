using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oplog.Persistence.Models
{
    public class OperationArea
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        //Note: use tag column from the legacy database
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public List<Unit> Units { get; } = new();
    }
}
