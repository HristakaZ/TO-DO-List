using System.ComponentModel.DataAnnotations;

namespace DataStructure.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<Activity> Activities { get; set; } = new List<Activity>();
    }
}
