namespace MyApp
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyApp.Core;
    using MyApp.Core.Interfaces;
    using MyApp.Data;
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            IServiceProvider services = ConfigureService();
            IEngine engine = new Engine(services);
            engine.Run();
        }

        private static IServiceProvider ConfigureService()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<MyAppContext>(db => db.UseSqlServer(Config.ConnectionString));

            //Adds a transient service of the type specified in serviceType to the specified 
            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();
            serviceCollection.AddTransient<Mapper>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
