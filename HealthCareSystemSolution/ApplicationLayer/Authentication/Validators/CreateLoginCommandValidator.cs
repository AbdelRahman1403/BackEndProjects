using ApplicationLayer.Authentication.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Authentication.Validators
{
    public class CreateLoginCommandValidator : AbstractValidator<CreateLoginCommand>
    {
        public CreateLoginCommandValidator()
        {
            RuleFor(x => x.dto).NotNull().WithMessage("Login data must be provided.");

            RuleFor(x => x.dto.UserName)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.dto.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
