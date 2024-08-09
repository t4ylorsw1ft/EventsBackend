using FluentValidation;
using MediatR;


namespace Events.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponce>
        : IPipelineBehavior<TRequest, TResponce>
        where TRequest : IRequest<TResponce>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponce> Handle(TRequest request, 
           RequestHandlerDelegate<TResponce> next, 
            CancellationToken cancellationToken)
        {
            Console.WriteLine("validation");
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();
            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
            return await next();
        }

    }

}
