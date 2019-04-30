using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MyApp.Core.Commands
{
    public class SetBirthdayCommand : ICommand
    {
        private readonly MyAppContext context;

        public SetBirthdayCommand(MyAppContext context)
        {
            this.context = context;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);
            DateTime date = DateTime.ParseExact(inputArgs[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var employee = this.context.Employees.Find(employeeId);

            if (IsValid(employee))
            {
                employee.Id = employeeId;
                employee.Birthday = date;

                this.context.SaveChanges();

                string result = "Birthdate added successfully!";

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
