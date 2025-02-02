using SimpleAPI.Models.UserDTO;

namespace SimpleAPI.Models.TaskDTO
{
    public class TaskItemResponse
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public UserResponse? Assignee { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
