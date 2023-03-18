using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp;

var services = new ServiceCollection();
DependencyInjectionSetup.ConfigureServices(services);

await using ServiceProvider serviceProvider = services.BuildServiceProvider();

// entry to run app
IEnumerable<Command> commands = serviceProvider.GetServices<Command>();
var rootCommand = new RootCommand(description: "Weather information using a fake weather service.");
commands.ToList().ForEach(command => rootCommand.AddCommand(command));

await rootCommand.InvokeAsync(args);
