using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Authorize]
    [Route("api/Assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly DataContext _context;

        public AssignmentController(DataContext context)
        {
            _context = context;
        }

        // Token-dəki NameIdentifier claim-indən cari istifadəçinin ID-sini götürür
        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignments()
        {
            var assignments = await _context.Assignments.ToListAsync();
            return Ok(assignments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] AssignmentDto dto)
        {
            var task = new Assignment
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                AssigneUserId = dto.AssigneUserId,
                CreatedByUserId = GetCurrentUserId()
            };

            _context.Assignments.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] AssignmentDto dto)
        {
            var task = await _context.Assignments.FindAsync(id);
            if (task == null) return NotFound(new { message = "Bu ID ilə tapşırıq tapılmadı." });

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.AssigneUserId = dto.AssigneUserId;

            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var task = await _context.Assignments.FindAsync(id);
            if (task == null) return NotFound(new { message = "Bu ID ilə tapşırıq tapılmadı." });

            _context.Assignments.Remove(task);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Silindi" });
        }
    }
}