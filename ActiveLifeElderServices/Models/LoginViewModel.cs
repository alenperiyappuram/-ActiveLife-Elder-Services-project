using System.ComponentModel.DataAnnotations;

namespace ActiveLifeElderServices.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        // NEW: ReturnUrl property
        public string? ReturnUrl { get; set; } // Nullable string to handle cases where it's not present
    }
}