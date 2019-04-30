namespace MyApp.Core.Commands
{
    using AutoMapper;
    using MyApp.Core.Commands.Contracts;
    using MyApp.Data;
    using MyApp.Models;
    using MyApp.ViewModels;
    using System;

    public class AddEmployeeCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public AddEmployeeCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            string firstName = inputArgs[0];
            string lastName = inputArgs[1];
            decimal salary;

            bool success = decimal.TryParse(inputArgs[2], out salary);

            if (success)
            {
                var employee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Salary = salary
                };

                this.context.Employees.Add(employee);
                this.context.SaveChanges();

                var employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);

                string result = $"Registered successfully: {employeeDto.FirstName} {employeeDto.LastName} - {employeeDto.Salary}";

                return result;
            }
            else
            {
                throw new Exception("Invalid input!");
            } 
        }
    }
}
