namespace BlazorApp2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string AuthProvider { get; set; } // "Local" or "Google"

        public string? PasswordHash { get; set; }

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
    }

}
