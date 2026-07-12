using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Tapşırıq Əlavə Etmək")]
        public async Task<ActionResult<Assignment>> AddTask(AssignmentDto dto)
        {
            var task = new Assignment
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                UserId = dto.UserId
            };

            _context.Assignments.Add(task);
            await _context.SaveChangesAsync();
            
            return Ok(task);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Bütün Tapşırıqlar")]
        public async Task<ActionResult<List<Assignment>>> GetTasks()
        {
            return await _context.Assignments.ToListAsync();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Tapşırığı Yenilə (Status dəyişmək üçün)")]
        public async Task<IActionResult> UpdateTask(int id, AssignmentDto dto)
        {
            var task = await _context.Assignments.FindAsync(id);
            if (task == null) return NotFound("Tapşırıq tapılmadı.");

            // DTO-dan gələn məlumatları yeniləyirik
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.UserId = dto.UserId;

            await _context.SaveChangesAsync();
            
            // Ok(task) qaytaraq ki, Swagger dəyişikliyi görsün
            return Ok(task);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Tapşırıq Silmək")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Assignments.FindAsync(id);
            if (task == null) return NotFound("Tapşırıq tapılmadı.");

            _context.Assignments.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}