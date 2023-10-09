using Microsoft.Extensions.DependencyInjection;
using Oplog.Core.Infrastructure;

namespace Oplog.IntegrationTests.Fakes.Infrastructure;

public class FakeEventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IList<Type> _whitelist;
    public IList<IEvent> RaisedEvents { get; }

    public FakeEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _whitelist = new List<Type>();
        RaisedEvents = new List<IEvent>();
    }
    public async Task RaiseEvent<T>(T @event) where T : IEvent
    {
        RaisedEvents.Add(@event);
        var handlers = _serviceProvider.GetServices<IEventHandler<T>>().ToList();
        if (!handlers.Any())
        {
            return;
        }

        foreach (var handler in handlers)
        {
            if (_whitelist.Contains(handler.GetType()))
                await handler.Handle(@event);
        }
    }
}
