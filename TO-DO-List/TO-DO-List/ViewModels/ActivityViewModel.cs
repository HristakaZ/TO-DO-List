using System.ComponentModel.DataAnnotations;
using DataStructure.Attributes;
using DataStructure.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TO_DO_List.ViewModels
{
    public class ActivityViewModel
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
        public DateTime DueDate { get; set; } = DateTime.Now;

        public List<SelectListItem> WarningLevels { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public int CategoryID { get; set; }
    }
}
