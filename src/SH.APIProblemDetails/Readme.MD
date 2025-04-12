# SH.APIProblemDetails 

 
 ## 介绍
 在webapi中使用ProblemDetails来返回错误信息的统一格式化 ，实现 9475 标准协议 的快速集成

## 使用方法 
> 请先安装 NuGet 包
```bash
dotnet add package SH.APIProblemDetails
```

## 配置 
服务注册 后  及已经配置 自定义 ProblemDetails 集成，因为使用异常情况下处理，所以需要全局异常开启

```csharp
builder.Services.AddProblemDetailsExceptionHandler();
...

var app = builder.Build();
app.UseExceptionHandler();


```

## 代码示例
```csharp
        [HttpGet("problemresult")]

        public async Task<IResult> problemresult()
        {
            return Results.Problem(new ProblemDetails
            {

                Title = "我的测试",
                Detail = "测试是否好事----------",
                Type = "自定义类型",
            });
        }


        [HttpGet("problemresult2")]
        public async Task<IResult> problemresult2()
        {
            throw new ProblemDetailsException("异常测试", "我的错误原因！");
        }
```
## 结果
~~~json
{
  "type": "Bad Request",
  "title": "异常测试",
  "status": 400,
  "detail": "我的错误原因！",
  "instance": "GET /api/Test/problemresult2",
  "traceId": "00-3dac82cc8cff67ee0359de2277d822c6-39cd5d1071080f64-00",
  "requestId": "0HNBPOQAQ1S81:00000005",
  "spanId": "39cd5d1071080f64"
}
~~~