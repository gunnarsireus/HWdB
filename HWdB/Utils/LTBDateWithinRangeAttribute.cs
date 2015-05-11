using System;
using System.ComponentModel.DataAnnotations;

namespace HWdB.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LTBDateWithinRangeAttribute : ValidationAttribute
    {
        public LTBDateWithinRangeAttribute(string eosDate, string minNbrOfDays)
        {
            EOSDate = eosDate;
            MinNbrOfDays = int.Parse(minNbrOfDays);
        }

        private string EOSDate { get; set; }
        private int MinNbrOfDays { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime earlierDate = (DateTime)value;

            DateTime laterDate = (DateTime)validationContext.ObjectType.GetProperty(EOSDate).GetValue(validationContext.ObjectInstance, null);

            int diff = (int)(laterDate - earlierDate).TotalDays;
            if (diff < 3653)
            {
                if (diff < MinNbrOfDays)
                {
                    return new ValidationResult("Service Period cannot be shorter than Repair Lead Time");
                }
                else
                {
                    return ValidationResult.Success;

                }
            }
            else
            {
                return new ValidationResult("Service Period cannot be longer than 10 years. Please change EoS or LTB.");
            }

        }
    }
}
