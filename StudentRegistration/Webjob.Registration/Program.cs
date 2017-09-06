using System;
using System.Diagnostics;
using System.IO;
using Logic;
using Logic.Services;
using Logic.Settings;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Webjob.Registration
{
    class Program
    {
        private const string environmentVariable = "ASPNETCORE_ENVIRONMENT";
        private const string developmentEnvironment = "Development";

        private static IConfigurationRoot _configuration; 
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            var serviceProvider = ConfigureApp(serviceCollection);

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

            var storageAccountConnectionString = _configuration.GetSection("ConnectionStrings").GetValue<string>("AzureWebJobsStorage");

            var jobHostConfiguration = new JobHostConfiguration
                {
                    DashboardConnectionString = storageAccountConnectionString,
                    StorageConnectionString = storageAccountConnectionString
                };
            jobHostConfiguration.Tracing.ConsoleLevel = TraceLevel.Verbose; // level to be displayed on all tracers
            jobHostConfiguration.Queues.BatchSize = 1;
            jobHostConfiguration.Queues.MaxDequeueCount = 3;
            jobHostConfiguration.Queues.MaxPollingInterval = TimeSpan.FromMinutes(1);
            jobHostConfiguration.JobActivator = new InversionOfControlJobActivator(serviceProvider);

            if (jobHostConfiguration.IsDevelopment)
            {
                jobHostConfiguration.UseDevelopmentSettings();
            }
            
            var host = new JobHost(jobHostConfiguration);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }

        public static IServiceProvider ConfigureApp(ServiceCollection serviceCollection)
        {
            var environment = Environment.GetEnvironmentVariable(environmentVariable);

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (!string.IsNullOrWhiteSpace(environment))
            {
                configurationBuilder.AddJsonFile($"appsettings.{environment}.json", optional: true);
            }

            _configuration = configurationBuilder.Build();

            
            serviceCollection.AddLogging();
            serviceCollection.AddDataLayer(_configuration, ServiceLifetime.Transient);
            serviceCollection.AddTransient<StudentFunctions>();
            serviceCollection.AddTransient<IStudentService, StudentService>();

            if (string.Compare(environment, developmentEnvironment, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                serviceCollection.AddTransient<ISearchLogic, DevelopmentSearchLogic>();                
            }
            else
            {
                serviceCollection.AddTransient<ISearchLogic, SearchLogic>();
            }

            serviceCollection.Configure<SearchSettings>(options => _configuration.GetSection("Search").Bind(options));
            var serviceProvider = serviceCollection.BuildServiceProvider();

            //configure console logging
            serviceProvider.GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Warning);
            
            return serviceProvider;
        }
    }
}
