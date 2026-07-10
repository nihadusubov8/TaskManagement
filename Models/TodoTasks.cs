using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class TodoTasks
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlıq sahəsi mütləq doldurulmalıdır.")]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
    }
}