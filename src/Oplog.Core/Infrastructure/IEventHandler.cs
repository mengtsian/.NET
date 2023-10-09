namespace Oplog.Core.Infrastructure;

public interface IEventHandler<T> where T : IEvent
{
    Task Handle(T @event);
}
