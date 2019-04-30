using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsPaymentSystem.Models.AdditionalAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExpirationDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currDateTime = DateTime.Now;

            if (currDateTime > (DateTime)value)
            {
                return new ValidationResult("The card has expired!");
            }

            return ValidationResult.Success;
        }
    }
}
