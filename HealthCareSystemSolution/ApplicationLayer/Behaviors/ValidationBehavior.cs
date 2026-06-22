using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var Result = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context , cancellationToken)));

            var failures = Result.SelectMany(r => r.Errors).Where(f => f is not null).ToList();

            if(failures.Any())
                throw new ValidationException(failures);
            return await next();

        }
    }
}
