using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oplog.Api.Models
{
    public class GetSearchLogsRequest
    {
        private const int DefaultPageSize = 250;
        private const int InitialPageNumber = 1;
        public int[] LogTypeIds { get; set; }
        public int[] AreaIds { get; set; }
        public int[] SubTypeIds { get; set; }
        public int[] UnitIds { get; set; }
        public string SearchText { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        public List<string> SortBy { get; set; }
        public int PageSize { get; set; } = DefaultPageSize;
        public int PageNumber { get; set; } = InitialPageNumber;
    }
}
