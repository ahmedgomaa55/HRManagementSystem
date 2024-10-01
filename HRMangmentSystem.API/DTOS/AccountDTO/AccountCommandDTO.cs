using System.ComponentModel.DataAnnotations;

namespace HRMangmentSystem.API.DTOS.AccountDTO
{
    public class AccountCommandDTO
    {
        [MaxLength(45, ErrorMessage = "Full name must not exceed 45 characters.")]
        public string FullName { get; set; }
        [MaxLength(20, ErrorMessage = "Username must not exceed 20 characters.")]
        [RegularExpression("^[a-zA-Z0-9_-]{4,20}$")]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        [RegularExpression("^[a-zA-Z0-9@#$%^&*!]{8,20}$")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        public int GroupId { get; set; }
    }
}
