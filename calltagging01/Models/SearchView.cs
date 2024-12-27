using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace calltagging01.Models
{
    public class SearchView
    {
        [Required]
        public string? ConnectionType { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public DateTime? FromDate { get; set; }
        [Required]
        public DateTime? ToDate { get; set; }
    }
}
