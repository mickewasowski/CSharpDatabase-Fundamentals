using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Core.Commands
{
    public class SetManagerCommand : ICommand
    {
        private readonly MyAppContext context;

        public SetManagerCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);
            int managerId = int.Parse(inputArgs[1]);

            var employee = this.context.Employees.Find(employeeId);
            var manager = this.context.Employees.Find(managerId);

            if (IsValid(employee) && IsValid(manager))
            {
                employee.Manager = manager;

                this.context.SaveChanges();

                return "Manager set successfully!";
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
