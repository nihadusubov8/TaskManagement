namespace TaskManagementApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Bu sətir mütləq olmalıdır:
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}