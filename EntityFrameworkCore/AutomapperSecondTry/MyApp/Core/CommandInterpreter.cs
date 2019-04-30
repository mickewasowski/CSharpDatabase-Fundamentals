namespace MyApp.Core
{
    using MyApp.Core.Commands.Contracts;
    using MyApp.Core.Interfaces;
    using System;
    using System.Linq;
    using System.Reflection;

    public class CommandInterpreter : ICommandInterpreter
    {
        private const string Suffix = "Command";
        private readonly IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Read(string[] inputArgs)
        {
            string commandName = inputArgs[0] + Suffix;
            string[] tokens = inputArgs.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name == commandName);

            if (type == null)
            {
                throw new ArgumentNullException("Invalid command!");
            }
            else
            {
                var constructor = type.GetConstructors().FirstOrDefault();

                var constructorParams = constructor.GetParameters().Select(x => x.ParameterType).ToArray();

                var services = constructorParams.Select(this.serviceProvider.GetService).ToArray();

                var command = (ICommand) constructor.Invoke(services);

                string result = command.Execute(tokens);

                return result;
            }
        }
    }
}
