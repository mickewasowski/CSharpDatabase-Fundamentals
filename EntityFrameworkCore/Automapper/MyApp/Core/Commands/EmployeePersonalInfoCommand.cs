using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyApp.Core.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public EmployeePersonalInfoCommand(MyAppContext context, Mapper mapper)
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

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}");
                sb.AppendLine($"Birthday: {employeeDto.Birthday}");
                sb.AppendLine($"Address: {employeeDto.Address}");

                return sb.ToString().TrimEnd();
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
