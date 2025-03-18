using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks([FromQuery] bool? completed)
        {
            var tasks = _context.Tasks.AsQueryable();

            if (completed.HasValue)
                tasks = tasks.Where(t => t.IsCompleted == completed.Value);

            return await tasks.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem updatedTask)
{
    if (updatedTask == null || id != updatedTask.Id)
    {
        return BadRequest(new { message = "ID no válido o datos incorrectos." });
    }

    var existingTask = await _context.Tasks.FindAsync(id);
    if (existingTask == null)
    {
        return NotFound(new { message = "Tarea no encontrada." });
    }

    // Forzar la asignación del ID correcto
    updatedTask.Id = id;
    
    // Mapear los valores actualizados
    existingTask.Title = updatedTask.Title;
    existingTask.Description = updatedTask.Description;
    existingTask.IsCompleted = updatedTask.IsCompleted;

    await _context.SaveChangesAsync();
    return Ok(existingTask);
}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}