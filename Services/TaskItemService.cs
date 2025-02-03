using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Data;
using SimpleAPI.Models.TaskDTO;
using SimpleAPI.Models.UserDTO;
using SimpleAPI.Services.Repository;

namespace SimpleAPI.Models
{
    public class TaskItemService
    {


        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IUserRepository _userRepository;

        Mapper mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TaskItem, TaskItemResponse>()
            .ForMember(dest => dest.Assignee, opt => opt.MapFrom(src => src.Assignee));
            cfg.CreateMap<User, UserResponse>();

            cfg.CreateMap<CreateTaskItemRequest, TaskItem>()
            .ForMember(dest => dest.Assignee, opt => opt.Ignore());

        }));

        public TaskItemService()
        {
            this._taskItemRepository = new TaskItemRepository(new SQLiteContext());
            this._userRepository = new UserRepository(new SQLiteContext());
        }

        public TaskItemService(ITaskItemRepository taskitemRepository, IUserRepository userRepository)
        {
            this._taskItemRepository = taskitemRepository;
            this._userRepository = userRepository;
        }

        public async Task<IEnumerable<TaskItemResponse>> GetTaskItemsAsync()
        {
            return mapper.Map<IEnumerable<TaskItemResponse>>(await _taskItemRepository.GetTaskItemsAsync());
        }


        public async Task<TaskItemResponse> GetTaskItemAsync(int id)
        {
            var taskItem = await _taskItemRepository.GetTaskItemByIDAsync(id);
            if(taskItem == null)
            {
                throw new KeyNotFoundException("Task Item not found");
            }

            return mapper.Map<TaskItemResponse>(taskItem);
        }


        public async Task<TaskItemResponse> UpdateTaskItemAsync(int id, UpdateTaskItemRequest updateTaskItem)
        {
            TaskItem taskItem = await _taskItemRepository.GetTaskItemByIDAsync(id);
            if (taskItem == null)
            {
                throw new KeyNotFoundException("Task Item not found.");
            }


            if (!string.IsNullOrEmpty(updateTaskItem.Title))
            {
                taskItem.Title = updateTaskItem.Title;
            }

            if (updateTaskItem.Description == "Remove")
            {
                taskItem.Description = null;
            }
            else if (updateTaskItem.Description != null)
            {
                taskItem.Description = updateTaskItem.Description;
            }

            if (updateTaskItem.AssigneeId == 0)
            {
                taskItem.Assignee = null;
            }
            else if (updateTaskItem.AssigneeId != null)
            {
                User? assignee = await _userRepository.GetUserByIDAsync(updateTaskItem.AssigneeId.Value);

                if (assignee != null)
                {
                    taskItem.Assignee = assignee;
                }
                else
                {
                    throw new KeyNotFoundException("User with the provided AssigneeId not found.");
                }
            }

            if (updateTaskItem.DueDate != null && updateTaskItem.DueDate == DateTime.MinValue)
            {
                taskItem.DueDate = null;
            }
            else if (updateTaskItem.DueDate != null)
            {
                taskItem.DueDate = updateTaskItem.DueDate;
            }

            await _taskItemRepository.UpdateTaskItemAsync(taskItem);

            await _taskItemRepository.SaveAsync();

            return mapper.Map<TaskItemResponse>(taskItem);
        }


        public async Task<TaskItemResponse> CreateTaskItemAsync(CreateTaskItemRequest createTaskItem)
        {
            TaskItem taskItem = mapper.Map<TaskItem>(createTaskItem);

            if (createTaskItem.AssigneeId.HasValue)
            {
                var assignee = await _userRepository.GetUserByIDAsync(createTaskItem.AssigneeId.Value);
                taskItem.Assignee = assignee;  // Assign the user to the TaskItem
            }

            await _taskItemRepository.InsertTaskItemAsync(taskItem);
            await _taskItemRepository.SaveAsync();

            return mapper.Map<TaskItemResponse>(taskItem);
        }

        public async Task DeleteTaskItemAsync(int id)
        {

            await _taskItemRepository.DeleteTaskItemAsync(id);
            await _taskItemRepository.SaveAsync();


        }


        public async Task<IEnumerable<TaskItemResponse>> GetExpiredTasksAsync()
        {
            var taskItems = await _taskItemRepository.GetTaskItemsAsync();
            return mapper.Map<IEnumerable<TaskItemResponse>>(taskItems.Where(t => t.DueDate < DateTime.Now).ToList());
        }

        public async Task<IEnumerable<TaskItemResponse>> GetActiveTasksAsync()
        {
            var taskItems = await _taskItemRepository.GetTaskItemsAsync();
            return mapper.Map<IEnumerable<TaskItemResponse>>(taskItems.Where(t => t.DueDate >= DateTime.Now).ToList());
        }

        public async Task<IEnumerable<TaskItemResponse>> GetTasksFromDateAsync(DateTime date)
        {
            var taskItems = await _taskItemRepository.GetTaskItemsAsync();
            return mapper.Map<IEnumerable<TaskItemResponse>>(taskItems.Where(t => t.DueDate >= date).ToList());
        }


    }
}
