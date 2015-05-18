using HWdB.DataAccess;
using HWdB.Model;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HWdB.CustomValidationAttributes
{
    public class VersionUniqueAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string version = (string)value;
                string customer = (string)validationContext.ObjectType.GetProperty("Customer").GetValue(validationContext.ObjectInstance, null);

                int dataSetId = (int)validationContext.ObjectType.GetProperty("ID").GetValue(validationContext.ObjectInstance, null);

                if (dataSetId > 0) return ValidationResult.Success;

                using (var context = new DataContext())
                {
                    LtbDataSet stored = context.LtbDataSets.Where(a => ((a.Customer == customer) && (a.Version == version))).FirstOrDefault();
                    if (stored != null)
                    {
                        return new ValidationResult("Version already exists");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            else
            {
                return new ValidationResult("Version cannot be empty");
            }
        }
    }
}

