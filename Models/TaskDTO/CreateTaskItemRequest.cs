namespace SimpleAPI.Models.TaskDTO
{
    public class CreateTaskItemRequest
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int? AssigneeId { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
