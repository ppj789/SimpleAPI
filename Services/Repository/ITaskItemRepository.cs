using SimpleAPI.Models;

namespace SimpleAPI.Services.Repository
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetTaskItemsAsync();
        Task<TaskItem> GetTaskItemByIDAsync(int taskitemId);
        Task InsertTaskItemAsync(TaskItem taskitem);
        Task DeleteTaskItemAsync(int taskitemId);
        Task UpdateTaskItemAsync(TaskItem taskitem);
        Task SaveAsync();
    }
}
