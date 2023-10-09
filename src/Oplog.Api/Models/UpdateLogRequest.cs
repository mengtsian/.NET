using System;
using System.ComponentModel.DataAnnotations;

namespace Oplog.Api.Models
{
    public class UpdateLogRequest
    {
        [Required]
        public int? LogType { get; set; }
        public int SubType { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int? OperationsAreaId { get; set; }
        public string Author { get; set; }
        [Required]
        public int? Unit { get; set; }
        [Required]
        public DateTime? EffectiveTime { get; set; }
        public bool? IsCritical { get; set; }
    }
}
