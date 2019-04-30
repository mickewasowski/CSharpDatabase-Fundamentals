using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Core.Commands
{
    public class EmployeeInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public EmployeeInfoCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            var employeeId = int.Parse(inputArgs[0]);

            var employee = this.context.Employees.Find(employeeId);

            if (IsValid(employee))
            {
                var employeeDto = this.mapper.CreateMappedObject<Employee>(employee);

                string result = $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} -  ${employeeDto.Salary:f2}";

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
