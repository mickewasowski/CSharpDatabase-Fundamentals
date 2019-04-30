namespace MyApp.Core.Commands
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using MyApp.Core.Commands.Contracts;
    using MyApp.Data;
    using MyApp.ViewModels;
    using System.Linq;
    using System.Text;

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
            int employeeId;
            bool success = int.TryParse(inputArgs[0], out employeeId);

            if (success)
            {
                var manager = this.context.Employees.Include(m => m.ManagedEmployees).FirstOrDefault(x => x.Id == employeeId);

                if (manager != null)
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
            }

            return "Manager does not exist!";
        }
    }
}
