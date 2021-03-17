using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Rootstock.Core
{
    public static class LoggerFactoryProvider
    {
        public static ILoggerFactory GetLoggerFactory(this IServiceCollection self)
        {
            var internalServiceProvider = new DefaultServiceProviderFactory().CreateServiceProvider(self);

            try
            {
                // If an ILoggerFactory is not registered and an exception is thrown when resolving
                // This an an acceptable state of the system
                // a NullLoggerFactory should be used in the event the host application does not provide an ILoggerFactory
                return internalServiceProvider.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
            }
            catch (InvalidOperationException)
            {
                return NullLoggerFactory.Instance;
            }
        }
    }
}