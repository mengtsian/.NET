using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Oplog.Core.AzureSearch;
using Oplog.Core.Infrastructure;
using Oplog.Core.Queries;
using Oplog.IntegrationTests.Fakes.AzureSearch;
using Oplog.IntegrationTests.Fakes.Infrastructure;
using Oplog.Persistence;
using Oplog.Persistence.Repositories;

namespace Oplog.IntegrationTests;

public class TestBase
{
    protected IHost Host { get; set; }
    protected ICommandDispatcher CommandDispatcher { get; private set; }
    protected FakeEventDispatcher EventDispatcher { get; private set; }

    [SetUp]
    public void SetUp()
    {
        Host = new HostBuilder()
            .UseEnvironment("Development")
            .ConfigureHostConfiguration(config => config.AddConfiguration(TestRunSetUp.Configuration))
            .ConfigureServices(services =>
            {
                services.
                    AddDbContext<OplogDbContext>(options =>
                    {
                        options.UseSqlServer(TestRunSetUp.ConnectionString);
                        options.EnableSensitiveDataLogging();
                    });
                services.AddScoped<ICommandDispatcher, CommandDispatcher>();
                services.AddScoped<IEventDispatcher, FakeEventDispatcher>();
                services.AddTransient<ILogsRepository, LogsRepository>();
                services.AddTransient<IOperationsAreasRepository, OperationAreasRepository>();
                services.AddTransient<IConfiguredTypesRepository, ConfiguredTypesRepository>();
                services.AddTransient<ICustomFilterRepository, CustomFilterRepository>();
                services.AddTransient<ILogTemplateRepository, LogTemplateRepository>();
                services.AddTransient<ILogsQueries, LogsQueries>();
                services.AddTransient<IOperationAreasQueries, OperationAreasQueries>();
                services.AddTransient<IConfiguredTypesQueries, ConfiguredTypesQueries>();
                services.AddTransient<ICustomFilterQueries, CustomFilterQueries>();
                services.AddTransient<ILogTemplateQueries, LogTemplateQueries>();
                services.AddSingleton<IIndexDocumentClient, FakeIndexDocumentClient>();
                AddCommandHandlers(services, typeof(ICommandHandler<>));
                AddCommandHandlers(services, typeof(ICommandHandler<,>));
                AddEventHandlers(services, typeof(IEventHandler<>));
            })
            .Build();

        CommandDispatcher = Host.Services.GetService(typeof(ICommandDispatcher)) as ICommandDispatcher;
        EventDispatcher = Host.Services.GetService(typeof(IEventDispatcher)) as FakeEventDispatcher;
    }

    private static void AddCommandHandlers(IServiceCollection services, Type interfaceType)
    {
        var types = interfaceType.Assembly.GetTypes().Where(t =>
            t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType));
        foreach (var type in types)
        {
            type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
                .ToList().ForEach(i => services.AddScoped(i, type));
        }
    }

    private static void AddEventHandlers(IServiceCollection services, Type handlerInterface)
    {
        var handlers = typeof(IEvent).Assembly.GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            );

        foreach (var handler in handlers)
        {
            handler.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
                .ToList().ForEach(i => services.AddScoped(i, handler));
        }
    }
}
