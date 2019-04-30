namespace MyApp.Core
{
    using MyApp.Core.Interfaces;
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string[] inputArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

                    ICommandInterpreter commandInterpreter = this.serviceProvider.GetService<ICommandInterpreter>();

                    string result = commandInterpreter.Read(inputArgs);

                    Console.WriteLine(result);
                }
                catch (Exception ms)
                {
                    Console.WriteLine(ms);
                }
            }
        }
    }
}
