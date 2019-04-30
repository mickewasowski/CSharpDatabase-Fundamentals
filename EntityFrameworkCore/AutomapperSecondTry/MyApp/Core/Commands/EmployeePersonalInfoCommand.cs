namespace MyApp.Core.Commands
{
    using AutoMapper;
    using MyApp.Core.Commands.Contracts;
    using MyApp.Data;
    using MyApp.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public EmployeePersonalInfoCommand(MyAppContext contex, Mapper mapper)
        {
            this.context = contex;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId;
            bool success = int.TryParse(inputArgs[0], out employeeId);

            if (success)
            {
                var employee = this.context.Employees.Find(employeeId);

                if (employee != null)
                {
                    var employeeDto = this.mapper.CreateMappedObject<ExtendedEmployeeDto>(employee);

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine($"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}");
                    sb.AppendLine($"Birthday: {employeeDto.Birthday.ToString("dd-MM-yyyy")}");
                    sb.AppendLine($"Address: {employeeDto.Address}");

                    return sb.ToString().TrimEnd();
                }
            }

            return "Employee does not exist!";
        }
    }
}
