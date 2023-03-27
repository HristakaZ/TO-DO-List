using System.ComponentModel.DataAnnotations;

namespace DataStructure.Attributes
{
    internal class ValidDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = Convert.ToDateTime(value);

            return date >= DateTime.Now;
        }
    }
}
