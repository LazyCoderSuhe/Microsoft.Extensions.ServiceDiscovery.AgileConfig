using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ServiceDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using agile = AgileConfig.Client;

namespace SH.Microsoft.Extensions.ServiceDiscovery.AgileConfig
{
    public static class HostingExtensions
    {
        /// <summary>
        /// Add AgileConfig Service Discovery
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAgileConfigServiceEndpointProvider(this IServiceCollection services)
        {
            services.AddServiceDiscoveryCore();
            services.AddSingleton<IServiceEndpointProviderFactory, AgileConfyServiceEndpointProviderFactory>();
            return services;
        }
        /// <summary>        
        /// Add AgileConfig Service Discovery 
        /// 服务IP 自动从 Dns.GetHostAddresses(Dns.GetHostName()) 获取
        /// 端口 默认8080
        /// 服务名 默认应用名   自动转成 小写
        /// 服务Id 默认配置ID+Guid
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostApplicationBuilder AddAgileConfigRegisterService(this IHostApplicationBuilder builder)
        {
            builder.Configuration.AddAgileConfig(ev =>
            {
                var config = ev.RegisterInfo;
                config = new agile.RegisterCenter.ServiceRegisterInfo
                {
                    // 获取主机IP
                    Ip = config?.Ip ?? Dns.GetHostAddresses(Dns.GetHostName())
                        .FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        ?.ToString(), // 获取主机IP
                    Port = config?.Port ?? 8080,

                    ServiceName = !string.IsNullOrWhiteSpace(config?.ServiceName) ? config.ServiceName.ToLower() : builder.Environment.ApplicationName.ToLower(),
                    ServiceId = config?.ServiceId + Guid.NewGuid().ToString(),
                };
                ev.RegisterInfo = config;
            }); // 添加 AgileConfig 配置源
            builder.Services.AddAgileConfig(); // 添加 AgileConfig 配置源
            return builder;
        }
    }
}
