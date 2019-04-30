namespace MyApp.Core.Commands
{
    using MyApp.Core.Commands.Contracts;
    using MyApp.Data;

    public class SetManagerCommand : ICommand
    {
        private readonly MyAppContext context;

        public SetManagerCommand(MyAppContext context)
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
                    int managerId = int.Parse(inputArgs[1]);
                    employee.ManagerId = managerId;

                    this.context.SaveChanges();

                    string result = "Manager set successfully!";

                    return result;
                }
            }

            return "Employee does not exist!";
        }
    }
}
