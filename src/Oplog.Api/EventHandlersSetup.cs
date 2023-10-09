using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Oplog.Api
{
    public static class EventHandlersSetup
    {
        public static void AddEventHandlers(IServiceCollection services, Type handlerInterface)
        {
            var handlers = handlerInterface.Assembly.GetTypes()
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
}
