using EShop.Shared.CQRS;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest :ICommand<TRequest>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any()) {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task
                    .WhenAll(validators.Select(x=>x.ValidateAsync(context,cancellationToken)));

                var errors=validationResults
                    .Where(x => x.Errors.Count != 0)
                    .SelectMany(r=>r.Errors).ToList();  
                throw new ValidationException(errors);
            }
            return await next();
        }
    }
}
