using System;
using System.ComponentModel.DataAnnotations;

namespace HWdB.Utils
{
    public class EOSDateWithinRangeAttribute : ValidationAttribute
    {
        public EOSDateWithinRangeAttribute(string ltbDate, string minNbrOfDays)
        {
            LTBDate = ltbDate;
            MinNbrOfDays = int.Parse(minNbrOfDays);
        }

        private string LTBDate { get; set; }
        private int MinNbrOfDays { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime laterDate = (DateTime)value;

            DateTime earlierDate = (DateTime)validationContext.ObjectType.GetProperty(LTBDate).GetValue(validationContext.ObjectInstance, null);

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
