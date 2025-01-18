using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task user)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Models.Task user)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {

            return Ok();
        }

    }
}
