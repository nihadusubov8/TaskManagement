namespace TaskManagementApi.Models // Yenə eyni namespace
{
    public class AssignmentDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int UserId { get; set; }
        public Status Status { get; set; } = Status.Todo;
    }
}