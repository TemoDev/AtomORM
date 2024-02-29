using AtomORM.Core;
using Microsoft.Extensions.DependencyInjection;

namespace AtomORM.DependencyInjection;

public static class DependencyInjectionExtension
{
    /// <summary>
    ///     Registers the given context as a service in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
    ///     </para>
    /// </remarks>
    public static IServiceCollection AddAtomContext<TContext>(
             this IServiceCollection serviceCollection,
             Action<AtomContextOptions> options
         )
             where TContext : AtomContext
    {
        AddCoreServices(serviceCollection, options);
        serviceCollection.AddScoped<TContext>();
        return serviceCollection;
    }

    private static void AddCoreServices(
        this IServiceCollection serviceCollection,
        Action<AtomContextOptions> options)
    {
        AtomContextOptions atomContextOptions = new();
        options(atomContextOptions);
        serviceCollection.AddSingleton(atomContextOptions);
    }
}