using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Core.Commands
{
    public class SetAddressCommand : ICommand
    {
        private readonly MyAppContext context;

        public SetAddressCommand(MyAppContext context)
        {
            this.context = context;
        }

        public string Execute(string[] inputArgs)
        {
            var employeeId = int.Parse(inputArgs[0]);
            var address = inputArgs[1];

            var employee = this.context.Employees.Find(employeeId);

            if (IsValid(employee))
            {
                employee.Address = address;

                this.context.SaveChanges();

                string result = "Address added succssefully!";

                return result;
            }
            else
            {
                throw new Exception("Invalid entity!");
            }
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            return isValid;
        }
    }
}
