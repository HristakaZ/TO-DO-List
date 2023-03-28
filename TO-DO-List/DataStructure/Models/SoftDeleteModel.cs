namespace DataStructure.Models
{
    public class SoftDeleteModel : BaseEntity
    {
        public bool IsDeleted { get; set; }
    }
}
