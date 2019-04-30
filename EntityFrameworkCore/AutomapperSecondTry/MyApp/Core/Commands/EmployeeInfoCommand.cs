namespace MyApp.Core.Commands
{
    using AutoMapper;
    using MyApp.Core.Commands.Contracts;
    using MyApp.Data;
    using MyApp.ViewModels;

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
            int employeeId;
            bool success = int.TryParse(inputArgs[0], out employeeId);

            if (success)
            {
                var employee = this.context.Employees.Find(employeeId);

                if (employee != null)
                {
                    var employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);

                    var result = $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} -  ${employeeDto.Salary:f2}";

                    return result;
                }        
            }

            return "Employee does not exist!";
        }
    }
}
