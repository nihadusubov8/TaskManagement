namespace TaskManagementApi.Models
{
    public class AssignmentDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int AssigneUserId { get; set; } // Kimə təyin olunur
        public Status Status { get; set; } = Status.Todo;
        // CreatedByUserId artıq DTO-da yox, JWT-dən alınacaq.
    }
}