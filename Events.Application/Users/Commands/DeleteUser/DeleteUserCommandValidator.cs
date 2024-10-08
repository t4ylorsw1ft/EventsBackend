﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator() 
        { 
            RuleFor(u => 
                u.Id).NotEqual(Guid.Empty);
            RuleFor(u =>
                u.Password).NotEmpty();
        }
    }
}
