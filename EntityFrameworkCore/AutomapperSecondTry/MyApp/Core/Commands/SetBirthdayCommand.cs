namespace MyApp.Core.Commands
{
    using MyApp.Core.Commands.Contracts;
    using MyApp.Data;
    using System;
    using System.Globalization;

    public class SetBirthdayCommand : ICommand
    {
        private readonly MyAppContext context;

        public SetBirthdayCommand(MyAppContext context)
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
                    DateTime birthday = DateTime.ParseExact(inputArgs[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    employee.Birthday = birthday;

                    this.context.SaveChanges();

                    return "Birthday set successfully!";
                }
            }

            return "Employee does not exist!";
        }
    }
}
