using System.ComponentModel.DataAnnotations;

namespace DataStructure.Models
{
    public class User : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$", ErrorMessage = "The password must contain 1 number, 1 lowercase letter, 1 uppercase letter and 1 special character and must be at least 8 characters long.")]
        public string Password { get; set; }

        public virtual List<Activity> Activities { get; set; } = new List<Activity>();
    }
}
