namespace Oplog.Core.Infrastructure;

public interface IEventDispatcher
{
    Task RaiseEvent<T>(T @event) where T : IEvent;
}
