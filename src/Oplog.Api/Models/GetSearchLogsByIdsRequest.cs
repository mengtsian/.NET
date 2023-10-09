using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oplog.Api.Models
{
    public class GetSearchLogsByIdsRequest
    {
        [Required]
        public List<int> LogIds { get; set; }
        public List<string> SortBy { get; set; }
    }
}
