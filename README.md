# Microsoft.Extensions.ServiceDiscovery 集成 AgileConfig

## 使用方法

> 请先安装 NuGet 包
```bash
dotnet add package SH.Microsoft.Extensions.ServiceDiscovery.AgileConfig
```

## 配置文件
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "AgileConfig": {
    "appId": "Sample",
    "secret": "Sample",
    "nodes": "http://localhost:6001/", //多个节点使用逗号分隔,
    "name": "Sample",
    "tag": "tag2",
    "env": "PROD", //DEV TEST STAGING PROD
    // 下面的配置是可选的 如果不配置 通过 AddAgileConfigRegisterService 方法自动注册服务 因为测试 原因，本地开发是需要根据你嫩 LanchSettings.json 里面的配置来注册服务的
    "serviceRegister": {  //服务注册信息，如果不配置该节点，则不会启动任何跟服务注册相关的服务 可选
      "serviceId": "net6", //服务id，全局唯一，用来唯一标示某个服务
      "serviceName": "Sample", //服务名，可以重复，某个服务多实例部署的时候这个serviceName就可以重复
      "ip": "127.0.0.1", //服务的ip 可选
      "port": 5046 //服务的端口 可选
    }
  }
}

```

## 配置代码
```csharp
配置客户端 或者强类型中鼎
builder.Services.AddHttpClient<CatalogServiceClient>(c =>
{
  c.BaseAddress = new("https://catalog"));
}).AddServiceDiscovery();

builder.Services.AddHttpClient("test", t =>
{
    t.BaseAddress = new Uri($"http://{builder.Environment.ApplicationName.ToLower()}");
}).AddServiceDiscovery();


// Add services to the container.
builder.Services.AddServiceDiscovery()
    .AddAgileConfigServiceEndpointProvider();
builder.AddAgileConfigRegisterService();
```
## 项目依赖
* https://github.com/kklldog/AgileConfig/tree/master 

实现的。