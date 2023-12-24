using System.ComponentModel.DataAnnotations;

namespace BlogApi.Validate;

public class DateValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime date = (DateTime)value;
        return date < DateTime.Now;
    }
}