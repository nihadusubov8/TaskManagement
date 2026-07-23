using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementApi.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlıq sahəsi mütləq doldurulmalıdır.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Status Status { get; set; } = Status.Todo;

        // Kim yaradıb
        public int CreatedByUserId { get; set; }
        
        [ForeignKey("CreatedByUserId")]
        public User CreatedByUser { get; set; } = null!;

        // Kimə təyin olunub
        public int AssigneUserId { get; set; }
        
        [ForeignKey("AssigneUserId")]
        public User AssigneUser { get; set; } = null!;
    }
}