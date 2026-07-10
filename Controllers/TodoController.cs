using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;
using Swashbuckle.AspNetCore.Annotations; // 1. Bu kitabxananı əlavə etdik

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly DataContext _context;

        public TodoController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Tapşırıq Əlavə Etmək")] // 2. Adı dəyişdik
        public async Task<ActionResult<TodoTasks>> AddTask(TodoTaskDto dto)
        {
            if (string.IsNullOrEmpty(dto.Title)) return BadRequest("Başlıq boş ola bilməz.");

            var task = new TodoTasks
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted,
                UserId = 1
            };

            _context.TodoTasks.Add(task);
            await _context.SaveChangesAsync();
            
            return Ok(task);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Aktiv Tapşırıqlar")] // Adı dəyişdik
        public async Task<ActionResult<List<TodoTasks>>> GetTasks()
        {
            return await _context.TodoTasks.ToListAsync();
        }

        [HttpGet("user/{userId}")]
        [SwaggerOperation(Summary = "İstifadəçinin Tapşırıqlarını Gör")] // Adı dəyişdik
        public async Task<ActionResult<List<TodoTasks>>> GetUserTasks(int userId)
        {
            return await _context.TodoTasks.Where(t => t.UserId == userId).ToListAsync();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Tapşırığı Yenilə")] // Adı dəyişdik
        public async Task<IActionResult> UpdateTask(int id, TodoTasks task)
        {
            if (id != task.Id) return BadRequest("ID uyğunsuzluğu.");
            
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Tapşırıq Silmək")] // Adı dəyişdik
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);
            if (task == null) return NotFound("Tapşırıq tapılmadı.");

            _context.TodoTasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}