using System;
using Microsoft.Extensions.DependencyInjection;
using ToyRobot.Models;
using ToyRobot.Services;

namespace ToyRobot
{
    class Program
    {
        private static bool _keepRunning = true;

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var robotService = serviceProvider.GetService<IRobotService>();

            Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e)
            {
               // If user press CTRL+C, app exited 
                e.Cancel = true;
                _keepRunning = false;
                Environment.Exit(0);
            };
            Console.WriteLine("Enter command for Robot:");
            while (_keepRunning)
            {
                // Read command from console
                var input = Console.ReadLine();
                robotService.Run(input);
            }

            Console.WriteLine("exited");
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Dependency Injection
            services.AddSingleton<IRobot, Robot>()
                .AddTransient<ICommandFactory, CommandFactory>()
                .AddTransient<IRobotService, RobotService>();

        }
    }
}
