using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using MyApp.Models;
using System;
using System.Linq;
using System.Text;

namespace MyApp.Core.Commands
{
    class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public ListEmployeesOlderThanCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int age = int.Parse(inputArgs[0]);

            var currDate = DateTime.Now.Year;

            Employee[] employees = this.context.Employees.Include(m => m.ManagedEmployees).Where(x => (currDate - x.Birthday.Value.Year) == age).ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var e in employees)
            {
                sb
                    .AppendLine($"{e.FirstName} {e.LastName} - ${e.Salary:f2} - Manager: [{(e.Manager.LastName != null ? e.Manager.LastName : "no manager")}]");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
