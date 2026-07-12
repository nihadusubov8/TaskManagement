using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models // Burası mütləq Status.cs ilə eyni olmalıdır!
{
    public class Assignment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlıq sahəsi mütləq doldurulmalıdır.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Artıq using TaskManagementApi.Models; yazmağa ehtiyac yoxdur, 
        // çünki ikisi də eyni namespace-dədir!
        public Status Status { get; set; } = Status.Todo; 

        public int UserId { get; set; }
    }
}