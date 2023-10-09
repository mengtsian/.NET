
namespace Oplog.Core.Infrastructure;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task Dispatch<T>(T command) where T : ICommand
    {
        if (_serviceProvider.GetService(typeof(ICommandHandler<T>)) is not ICommandHandler<T> handler)
        {
            throw new ApplicationException($"No Commandhandler registered for handling {typeof(T)}");
        }
        await handler.Handle(command);
    }

    public async Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand
    {
        return _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>)) is not ICommandHandler<TCommand, TResult> handler
            ? throw new ApplicationException($"No Commandhandler registered for handling {typeof(TCommand)}")
            : await handler.Handle(command);
    }
}
