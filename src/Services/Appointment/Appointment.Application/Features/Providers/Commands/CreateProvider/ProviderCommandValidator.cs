using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Application.Features.Providers.Commands.CreateProvider
{
    class ProviderCommandValidator : AbstractValidator<ProviderCommand>
    {

        public ProviderCommandValidator()
        {
            RuleFor(p => p.ProviderName)
                          .NotEmpty().WithMessage("{ProviderName} is required.")
                          .NotNull()
                          .MaximumLength(50).WithMessage("{ProviderName} must not exceed 50 characters.");
        }
         
    }
}
