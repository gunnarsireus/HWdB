using HWdB.DataAccess;
using HWdB.Model;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HWdB.CustomValidationAttributes
{
    public class UserNameUniqueAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string userName = (string)value;

                int userId = (int)validationContext.ObjectType.GetProperty("ID").GetValue(validationContext.ObjectInstance, null);

                if (userId > 0) return ValidationResult.Success;

                using (var context = new DataContext())
                {
                    User stored = context.Users.Where(a => (a.UserName == userName)).FirstOrDefault();
                    if (stored != null)
                    {
                        return new ValidationResult("Name already exists");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            else
            {
                return new ValidationResult("Name cannot be empty");
            }
        }
    }
}

