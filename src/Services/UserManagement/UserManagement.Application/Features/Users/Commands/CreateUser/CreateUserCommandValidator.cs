using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.FirstName)
              .NotEmpty().WithMessage("{FullName} is required.")
              .NotNull()
              .MaximumLength(50).WithMessage("{FullName} must not exceed 50 characters.");

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("{UserType} is required.");

            RuleFor(p => p.Password)
               .NotEmpty().WithMessage("{Password} is required.");

            RuleFor(p => p.ZipCode)
               .NotEmpty().WithMessage("{PasswordSalt} is required.");

        }
    }
}
