using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Core.Commands.Contracts;
using MyApp.Core.ViewModels;
using MyApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MyApp.Core.Commands
{
    public class ManagerInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public ManagerInfoCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public string Execute(string[] inputArgs)
        {
            int managerId = int.Parse(inputArgs[0]);

            var manager = this.context.Employees
                .Include(m => m.ManagedEmployees)
                .FirstOrDefault(x => x.Id == managerId);

            if (IsValid(manager))
            {
                var managerDto = this.mapper.CreateMappedObject<ManagerDto>(manager);

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.ManagedEmployees.Count}");

                foreach (var me in managerDto.ManagedEmployees)
                {
                    sb.AppendLine($"- {me.FirstName} {me.LastName} - ${me.Salary:f2}");
                }

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
