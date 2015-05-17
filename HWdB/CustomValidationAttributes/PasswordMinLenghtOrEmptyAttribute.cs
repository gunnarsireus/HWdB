using System.ComponentModel.DataAnnotations;

namespace MHWdB.CustomValidationAttributes
{
    public class PasswordMinLenghtOrEmptyAttribute : ValidationAttribute
    {
        int _minLength;
        public PasswordMinLenghtOrEmptyAttribute(int minLength)
        {
            _minLength = minLength;
        }
        protected override ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (value != null)
            {
                int userId = (int)validationContext.ObjectType.GetProperty("UserID").GetValue(validationContext.ObjectInstance, null);
                string password = (string)value;
                if ((userId > 0) && (password.Length == 0))
                {
                    return ValidationResult.Success;  //Old user already stored in db
                }
                else
                {
                    {//New user, password required
                        if (password.Length < _minLength)
                        {
                            return new ValidationResult("Password must be at least " + _minLength + " characters");
                        }
                        else
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
