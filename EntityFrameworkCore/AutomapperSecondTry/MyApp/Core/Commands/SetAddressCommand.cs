namespace MyApp.Core.Commands
{
    using MyApp.Core.Commands.Contracts;
    using MyApp.Data;
    using System.Linq;

    public class SetAddressCommand : ICommand
    {
        private readonly MyAppContext context;

        public SetAddressCommand(MyAppContext context)
        {
            this.context = context;
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
                    string[] address = inputArgs.Skip(1).ToArray();

                    employee.Address = string.Join(" ",address);

                    this.context.SaveChanges();

                    return "Address set successfully!";
                }
            }

            return "Employee does not exist!";
        }
    }
}
