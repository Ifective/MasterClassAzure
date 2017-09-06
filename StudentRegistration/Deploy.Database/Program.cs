using System;
using System.Diagnostics;
using System.Linq;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Deploy.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(LogLevel.Debug);
            var logger = loggerFactory.CreateLogger<Program>();

            if (!args.Any())
            {
                logger.LogError("No connectionstring supplied!");
                return;
            }

            var isDevelopment = false;

            if (args.Length > 1)
            {
                isDevelopment = args[1] == "development";
            }

            logger.LogInformation("Start database deployment");
            logger.LogInformation($"connection: {args[0]}");
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(args[0]);
            var context = new ApplicationDbContext(builder.Options);

            if (isDevelopment)
            {
                logger.LogWarning("WARNING: development mode active: DELETING DATABASE. ");
                logger.LogInformation("Abort with ctrl+c, continue with enter.");
                Console.ReadLine();
                logger.LogInformation("Start delete");
                context.Database.EnsureDeleted();
                logger.LogInformation("Finished delete");
            }

            logger.LogInformation("Start migration");
            context.Database.Migrate();
            logger.LogInformation("Finished migration");

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Finished, press any key to continue...");
                Console.ReadLine();
            }
        }
    }
}