using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Oplog.Api
{
    public static class CommandHandlersSetup
    {
        public static void AddCommandHandlers(IServiceCollection services, Type interfaceType)
        {
            var types = interfaceType.Assembly.GetTypes().Where(t =>
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType));
            foreach (var type in types)
            {
                type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
                    .ToList().ForEach(i => services.AddScoped(i, type));
            }
        }
    }
}
