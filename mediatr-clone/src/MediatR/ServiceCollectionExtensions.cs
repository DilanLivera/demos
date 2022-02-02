using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatRWithHandlersFromAssemblyContaining<TMarker>(
        this IServiceCollection services)
    {
        AddMediatRWithHandlersFromAssembliesContaining(services, typeof(TMarker));

        return services;
    }

    public static IServiceCollection AddMediatRWithHandlersFromAssembliesContaining(
        this IServiceCollection services, params Type[] assemblyMarkers)
    {
        assemblyMarkers = assemblyMarkers.Distinct().ToArray();

        if (!assemblyMarkers.Any())
        {
            throw new ArgumentException(
                "Assembly list is empty. Please provide one or more assemblies to scan for handlers.");
        }

        services.TryAddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.TryAddSingleton<IQueryDispatcher, QueryDispatcher>();

        var assemblies = assemblyMarkers.Select(type => type.Assembly).ToArray();

        var interfaceTypes = new[]
        {
            typeof(IQueryHandler<,>),
            typeof(ICommandHandler<,>)
        };

        foreach (var interfaceType in interfaceTypes)
        {
            var typeInfos = assemblies
                .SelectMany(assembly => assembly.DefinedTypes)
                .Where(typeInfo => typeof(IQueryHandler<,>).IsAssignableFrom(typeInfo) &&
                    !typeInfo.IsInterface &&
                    !typeInfo.IsAbstract)
                .ToList();

            foreach (var type in typeInfos)
            {
                services.AddTransient(interfaceType, type);
            }
        }

        return services;
    }
}
