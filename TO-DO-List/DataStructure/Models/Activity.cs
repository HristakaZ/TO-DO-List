using System.ComponentModel.DataAnnotations;
using DataStructure.Attributes;

namespace DataStructure.Models
{
    public class Activity
    {
        public int ID { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [MaxLength(600)]
        public string? Description { get; set; }

        public WarningLevel WarningLevel { get; set; }

        [Required]
        [ValidDate(ErrorMessage = "Invalid date.")]
        public DateTime DueDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual User User { get; set; } = new User();

        public virtual Category Category { get; set; } = new Category();
    }
}