﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HWdB.CustomValidationAttributes
{
    public class RepairLeadTimeEOSLTBRangeAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int MinNbrOfDays = (int)value;

            DateTime earlierDate = DateTime.Parse((string)validationContext.ObjectType.GetProperty("LTBDate").GetValue(validationContext.ObjectInstance, null));
            DateTime laterDate = DateTime.Parse((string)validationContext.ObjectType.GetProperty("EOSDate").GetValue(validationContext.ObjectInstance, null));

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
                return new ValidationResult("Service Period cannot be longer than 10 years.");
            }
        }
    }
}

