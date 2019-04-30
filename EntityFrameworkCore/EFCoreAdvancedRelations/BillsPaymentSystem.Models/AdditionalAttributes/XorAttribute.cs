using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsPaymentSystem.Models.AdditionalAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XorAttribute : ValidationAttribute
    {
        private string xorTargetAttribute;

        public XorAttribute(string xorTargetAttribute)
        {
            this.xorTargetAttribute = xorTargetAttribute;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //reflection... ;(
            var targetAttribute = validationContext.ObjectType
                .GetProperty(xorTargetAttribute)
                .GetValue(validationContext.ObjectInstance);

            if ((targetAttribute == null && value != null)||(targetAttribute != null && value == null))
            {
                return ValidationResult.Success;
            }

            string errorMsg = "The two properties must have opposite values!";

            return new ValidationResult(errorMsg);
        }
    }
}
