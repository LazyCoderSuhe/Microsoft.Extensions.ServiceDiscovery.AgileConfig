using AgileConfig.Client.RegisterCenter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ServiceDiscovery;
using System.Diagnostics.CodeAnalysis;

namespace SH.Microsoft.Extensions.ServiceDiscovery.AgileConfig
{
    /// <summary>
    /// AgileConfyServiceEndpointProviderFactory
    /// </summary>
    /// <param name="databaseService"></param>
    /// <param name="logger"></param>
    public class AgileConfyServiceEndpointProviderFactory(IDiscoveryService databaseService, ILogger<AgileConfyServiceEndpointProviderFactory> logger) : IServiceEndpointProviderFactory
    {
        private readonly IDiscoveryService databaseService = databaseService;
        private readonly ILogger<AgileConfyServiceEndpointProviderFactory> _logger= logger ;

        public bool TryCreateProvider(ServiceEndpointQuery query, [NotNullWhen(true)] out IServiceEndpointProvider? provider)
        {

            provider = new AgileConfyDiscoveryService(query, databaseService, _logger);
            return true;
        }
    }
}
