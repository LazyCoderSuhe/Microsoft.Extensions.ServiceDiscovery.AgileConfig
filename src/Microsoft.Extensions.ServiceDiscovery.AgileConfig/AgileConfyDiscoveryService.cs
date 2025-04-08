
using AgileConfig.Client.RegisterCenter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ServiceDiscovery;
using System.Net;
using System.Text.Json;

namespace SH.Microsoft.Extensions.ServiceDiscovery.AgileConfig
{
    /// <summary>
    /// AgileConfyDiscoveryService
    /// </summary>
    /// <param name="query"></param>
    /// <param name="databaseService"></param>
    /// <param name="logger"></param>
    public class AgileConfyDiscoveryService(ServiceEndpointQuery query, IDiscoveryService databaseService, ILogger logger) : IServiceEndpointProvider, IHostNameFeature
    {

        const string Name = "AgileConfig";
        private readonly string _serviceName = query.ServiceName;
        private readonly IDiscoveryService _databaseService = databaseService;
        private readonly ILogger _logger = logger;
        public string HostName => query.ServiceName;

        public ValueTask DisposeAsync() => default;

        public ValueTask PopulateAsync(IServiceEndpointBuilder endpoints, CancellationToken cancellationToken)
        {
            foreach (var endpoint in databaseService.GetByServiceName(_serviceName))
            {
                if (endpoint.Status == ServiceStatus.Healthy)
                    if (IPAddress.TryParse(endpoint.Ip, out var addr))
                    {

                        var endPoint = new IPEndPoint(addr, endpoint.Port ?? 8080);
                        var serviceEndPoint = ServiceEndpoint.Create(endPoint!);
                        serviceEndPoint.Features.Set<IServiceEndpointProvider>(this);
                        serviceEndPoint.Features.Set<IHostNameFeature>(this);
                        endpoints.Endpoints.Add(serviceEndPoint);
                        _logger.LogInformation("ServiceEndPointProvider Found Service {_serviceName}:{address}", _serviceName, addr);
                    }
            }

            return default;
        }
        public override string ToString() => Name;
    }
}