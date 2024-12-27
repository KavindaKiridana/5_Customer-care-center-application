using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace calltagging01.Models
{
    public class Call
    {
        [Key]
        public int Id { get; set; }
        public string? AgentPhone { get; set; } 
        [Required]
        public string? ConnectionType { get; set; }
        [Required]
        public string? Category { get; set; }
        [NotMapped]
        public List<string>? ProblemsList { get; set; }
        [Required]
        public string? Problems
        {
            get => ProblemsList != null ? string.Join(",", ProblemsList) : string.Empty;
            set => ProblemsList = value != null ? value.Split(',').ToList() : new List<string>();
        }
        public string? IssueType { get; set; }//this attribute could be null 
        [Required]
        public DateTime OccurredAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
