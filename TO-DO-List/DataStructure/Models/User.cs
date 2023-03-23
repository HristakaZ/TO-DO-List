using System.ComponentModel.DataAnnotations;

namespace DataStructure.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$", ErrorMessage = "The password must contain 1 number, 1 lowercase letter, 1 uppercase letter and 1 special character.")]
        public string Password { get; set; }

        public virtual List<Activity> Activities { get; set; } = new List<Activity>();
    }
}
