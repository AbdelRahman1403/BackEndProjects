using ApplicationLayer.Authentication.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Authentication.Validators
{
    public class CreateForgetPasswordValidator : AbstractValidator<CreateForgetPasswordCommand>
    {
        public CreateForgetPasswordValidator()
        {
            RuleFor(x => x.UserDto.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
            RuleFor(x => x.ResetSession)
                .NotEmpty();
        }
    }
}
