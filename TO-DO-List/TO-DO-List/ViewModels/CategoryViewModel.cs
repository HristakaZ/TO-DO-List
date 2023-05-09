using DataStructure.Models;
using System.ComponentModel.DataAnnotations;

namespace TO_DO_List.ViewModels
{
    public class CategoryViewModel : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
