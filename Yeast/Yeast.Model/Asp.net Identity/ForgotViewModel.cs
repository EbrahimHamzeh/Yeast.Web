using System.ComponentModel.DataAnnotations;

namespace Yeast.Model
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}