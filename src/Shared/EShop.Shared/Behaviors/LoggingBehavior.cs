using MediatR;
using Microsoft.Extensions.Logging;

namespace EShop.Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling request: {@Request}", request);
        var response=await next();
        logger.LogInformation("Handled request: {@Request} with response: {@Response}", request, response);
        return response;
    }
}