using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.UserName)
             .NotEmpty().WithMessage("{FullName} is required.")
             .NotNull()
             .MaximumLength(50).WithMessage("{FullName} must not exceed 50 characters.");

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{EmailAddress} is required.");
        }
    }
}
