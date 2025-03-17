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
[HttpPut("tasks/{id}")]
public IActionResult UpdateTask(int id, [FromBody] TaskItem updatedTask)
{
    // Verificar si el ID proporcionado coincide con el ID de la tarea en el cuerpo de la solicitud
    if (id != updatedTask.Id)
    {
        return BadRequest("El ID de la tarea no coincide con el ID proporcionado en la URL.");
    }

    // Buscar la tarea existente en la base de datos
    var existingTask = _context.Tasks.Find(id);
    if (existingTask == null)
    {
        return NotFound("La tarea con el ID proporcionado no fue encontrada.");
    }

    // Actualizar las propiedades de la tarea existente con los valores de la tarea actualizada
    existingTask.Title = updatedTask.Title;
    existingTask.Description = updatedTask.Description;
    existingTask.IsCompleted = updatedTask.IsCompleted;

    // Guardar los cambios en la base de datos
    _context.SaveChanges();

    // Retornar una respuesta sin contenido para indicar que la actualización fue exitosa
    return NoContent();
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