using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.EmailSubscription.Commands.Subscription
{
  public class SubscribeUserCommandValidator : AbstractValidator<SubscribeUserCommand>
    {
        public SubscribeUserCommandValidator()
        {
            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{EmailAddress} is required.");
        }

    }
}
