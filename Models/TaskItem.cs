using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.Models
{
    public class TaskItem
    {
        [Key]
        public required int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public User? Assignee { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
