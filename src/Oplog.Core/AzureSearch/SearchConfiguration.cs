using System.ComponentModel.DataAnnotations;

namespace Oplog.Core.AzureSearch;

public sealed class SearchConfiguration
{
    [Required]
    public string Endpoint { get; set; }
    [Required]
    public string AdminKey { get; set; }
    [Required]
    public string QueryKey { get; set; }
    [Required]
    public string SearchIndexName { get; set; }
}
