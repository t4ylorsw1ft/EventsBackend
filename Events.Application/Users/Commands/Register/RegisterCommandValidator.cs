using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Users.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(u =>
               u.Name).NotEmpty().MaximumLength(50);
            RuleFor(u =>
               u.Surname).NotEmpty().MaximumLength(50);
            RuleFor(u =>
               u.BirthDate).NotEmpty().LessThan(DateTime.Today);
            RuleFor(u =>
                u.Email).NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(u =>
                u.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        }
    }
}
