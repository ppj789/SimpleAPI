using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;
using SimpleAPI.Models.TaskDTO;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskItemsController : ControllerBase
    {
        private readonly TaskItemService _taskItemService;

        public TaskItemsController(TaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        // GET: api/TaskItems
        [HttpGet]
        public async Task<IEnumerable<TaskItemResponse>> GetTasks()
        {
            return await _taskItemService.GetTaskItemsAsync();
        }

        // GET: api/TaskItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemResponse>> GetTaskItem(int id)
        {
            var taskItem = await _taskItemService.GetTaskItemAsync(id);

          

            return taskItem;
        }

        // PUT: api/TaskItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItemResponse>> PutTaskItem(int id, UpdateTaskItemRequest updateTaskItem)
        {

            return await _taskItemService.UpdateTaskItemAsync(id, updateTaskItem);
        }

        // POST: api/TaskItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskItemResponse>> PostTaskItem(CreateTaskItemRequest createTaskItem)
        {

            return await _taskItemService.CreateTaskItemAsync(createTaskItem);
        }

        // DELETE: api/TaskItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            await _taskItemService.DeleteTaskItemAsync(id);

            return NoContent();
        }


        [HttpGet("expired")]
        public async Task<IActionResult> GetExpiredTasks()
        {
            var tasks = await _taskItemService.GetExpiredTasksAsync();
            return Ok(tasks);
        }


        [HttpGet("active")]
        public async Task<IActionResult> GetActiveTasks()
        {
            var tasks = await _taskItemService.GetActiveTasksAsync();
            return Ok(tasks);
        }

        //Phrasing is a bit confusing. Either, looking for due date so equals, or still with due time period from certain date. Going with the later.
        [HttpGet("fromDate")]
        public async Task<IActionResult> GetTasksFromDate(DateTime date)
        {
            var tasks = await _taskItemService.GetTasksFromDateAsync(date);
            return Ok(tasks);
        }

    }
}
