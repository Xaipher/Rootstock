using Microsoft.Extensions.Logging;

namespace Rootstock.Core
{
    public class AppDomainProcessor
    {
        private readonly ILogger<AppDomainProcessor> _logger;
        private readonly RootstockOptions _options;
        
        private AppDomainProcessor(ILoggerFactory loggerFactory, RootstockOptions options)
        {
            _logger = loggerFactory.CreateLogger<AppDomainProcessor>();
            _options = options;
        }

        public static AppDomainProcessor Build(ILoggerFactory loggerFactory, RootstockOptions options)
        {
            return new AppDomainProcessor(loggerFactory, options);
        }

        /// <summary>
        /// Eager loads all referenced assemblies in host application
        /// </summary>
        /// <returns></returns>
        public void InitializeAppDomain()
        {
            _logger.LogDebug("Eager loading referenced assemblies into current AppDomain");
            AppDomainEagerLoader.LoadReferencedAssemblies();

            if (_options.IncludeUnreferencedAssemblies)
            {
                AppDomainEagerLoader.LoadUnreferencedAssemblies(_options);
            }
        }
    }
}