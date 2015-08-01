using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MHWdB.CustomValidationAttributes
{
    public class ExcludeCharAttribute : ValidationAttribute
    {
        string _characters;
        public ExcludeCharAttribute(string characters)
        {
            _characters = characters;
        }
        protected override ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (value != null)
            {
                for (int i = 0; i < _characters.Length; i++)
                {
                    var valueAsString = value.ToString();
                    if (valueAsString.Contains(_characters[i]))
                    {
                        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(errorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
