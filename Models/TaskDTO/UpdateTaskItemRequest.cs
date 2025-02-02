namespace SimpleAPI.Models.TaskDTO
{
    public class UpdateTaskItemRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? AssigneeId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
