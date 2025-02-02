using Microsoft.EntityFrameworkCore;
using SimpleAPI.Data;
using SimpleAPI.Models;

namespace SimpleAPI.Services.Repository
{
    public class TaskItemRepository : ITaskItemRepository, IDisposable
    {

        private readonly SQLiteContext _context;

        public TaskItemRepository(SQLiteContext context)
        {
            _context = context;
        }

        public async Task DeleteTaskItemAsync(int taskitemId)
        {
            TaskItem taskItem = await GetTaskItemByIDAsync(taskitemId);
            _context.TaskItems.Remove(taskItem);
        }


        public async Task<TaskItem> GetTaskItemByIDAsync(int taskitemId)
        {
            return await _context.TaskItems.Include(t => t.Assignee).FirstOrDefaultAsync(t => t.Id == taskitemId);
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItemsAsync()
        {
            return await _context.TaskItems.Include(t => t.Assignee).ToListAsync();
        }

        public async Task InsertTaskItemAsync(TaskItem taskitem)
        {
            await _context.TaskItems.AddAsync(taskitem);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskItemAsync(TaskItem taskitem)
        {
            _context.Entry(taskitem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
