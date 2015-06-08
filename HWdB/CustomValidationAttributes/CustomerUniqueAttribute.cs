using HWdB.DataAccess;
using HWdB.Model;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HWdB.CustomValidationAttributes
{
    public class CustomerUniqueAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string customer = (string)value;
                string version = (string)validationContext.ObjectType.GetProperty("Version").GetValue(validationContext.ObjectInstance, null);

                int dataSetId = (int)validationContext.ObjectType.GetProperty("ID").GetValue(validationContext.ObjectInstance, null);

                if (dataSetId > 0) return ValidationResult.Success;

                using (var context = new DataContext())
                {
                    LtbDataSet stored = context.LtbDataSets.FirstOrDefault(a => ((a.Customer == customer) && (a.Version == version)));
                    if (stored != null)
                    {
                        return new ValidationResult("Customer already exists");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            else
            {
                return new ValidationResult("Customer cannot be empty");
            }
        }
    }
}

