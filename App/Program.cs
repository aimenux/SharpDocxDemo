using App.Commands;
using App.Options;
using Lib.Builders;
using Lib.Generators;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public static class Program
    {
        public static Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            var commands = host.Services
                .GetServices<ICommand>()
                .OfType<CommandLineApplication>()
                .ToList();

            var app = new CommandLineApplication
            {
                Name = "SharpDocxDemo",
                Description = "Using SharpDocx to generate docs based on templates"
            };

            app.Commands.AddRange(commands);

            app.HelpOption("-?|-h|--help");

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 0;
            });

            return app.ExecuteAsync(args);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddCommandLine(args);
                    config.AddEnvironmentVariables();
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureLogging((hostingContext, loggingBuilder) =>
                {
                    loggingBuilder.AddConsoleLogger();
                    loggingBuilder.AddNonGenericLogger();
                    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddSingleton<OutputFilePathOption>();
                    services.AddSingleton<IModelBuilder, ModelBuilder>();
                    services.AddSingleton<ICommand, DocumentGeneratorCommand>();
                    services.AddSingleton<IDocumentGenerator, DocumentGenerator>();
                });

        private static void AddConsoleLogger(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole(options =>
            {
                options.DisableColors = false;
                options.TimestampFormat = "[HH:mm:ss:fff] ";
            });
        }

        private static void AddNonGenericLogger(this ILoggingBuilder loggingBuilder)
        {
            var services = loggingBuilder.Services;
            services.AddSingleton(serviceProvider =>
            {
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                return loggerFactory.CreateLogger("SharpDocxDemo");
            });
        }
    }
}
