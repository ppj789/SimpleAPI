using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Data;
using SimpleAPI.Services.Repository;
using System.Collections.Generic;

namespace SimpleAPI.Models
{
    public class TaskItemService
    {

        private readonly ITaskItemRepository _taskItemRepository;

        public TaskItemService()
        {
            this._taskItemRepository = new TaskItemRepository(new SQLiteContext());
        }

        public TaskItemService(ITaskItemRepository taskitemRepository)
        {
            this._taskItemRepository = taskitemRepository;
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItemsAsync()
        {
            return await _taskItemRepository.GetTaskItemsAsync();
        }


        public async Task<TaskItem> GetTaskItemAsync(int id)
        {
            var taskItem = await _taskItemRepository.GetTaskItemByIDAsync(id);

            return taskItem;
        }


        public async Task<TaskItem> UpdateTaskItemAsync(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                throw new ArgumentException("Task ID does not match.");
            }

            await _taskItemRepository.UpdateTaskItemAsync(taskItem);

            try
            {
                await _taskItemRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(id))
                {
                    throw new KeyNotFoundException("Task Item not found.");
                }
                else
                {
                    throw;
                }
            }

            return taskItem;
        }


        public async Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem)
        {
            await _taskItemRepository.InsertTaskItemAsync(taskItem);
            await _taskItemRepository.SaveAsync();

            return taskItem;
        }

        public async Task DeleteTaskItemAsync(int id)
        {
            var taskItem = await _taskItemRepository.GetTaskItemByIDAsync(id);
            if (taskItem == null)
            {
                throw new KeyNotFoundException("Task Item not found.");
            }

            await _taskItemRepository.DeleteTaskItemAsync(id);
            await _taskItemRepository.SaveAsync();

          
        }

        private bool TaskItemExists(int id)
        {
            return _taskItemRepository.GetTaskItemByIDAsync(id) != null;
        }


        public async Task<IEnumerable<TaskItem>> GetExpiredTasksAsync()
        {
            var taskItems = await _taskItemRepository.GetTaskItemsAsync();
            return taskItems.Where(t => t.DueDate < DateTime.Now).ToList();
        }

        public async Task<IEnumerable<TaskItem>> GetActiveTasksAsync()
        {
            var taskItems = await _taskItemRepository.GetTaskItemsAsync();
            return taskItems.Where(t => t.DueDate >= DateTime.Now).ToList();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksFromDateAsync(DateTime date)
        {
            var taskItems = await _taskItemRepository.GetTaskItemsAsync();
            return taskItems.Where(t => t.DueDate >= date).ToList();
        }


    }
}
