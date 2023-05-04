using System.ComponentModel.DataAnnotations;

namespace DataStructure.Attributes
{
    public class ValidDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = Convert.ToDateTime(value);

            return date.Date >= DateTime.Now.Date;
        }
    }
}
