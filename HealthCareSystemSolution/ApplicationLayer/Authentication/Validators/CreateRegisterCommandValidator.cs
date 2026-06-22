using ApplicationLayer.Authentication.Commands;
using FluentValidation;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Authentication.Validators
{
    public class CreateRegisterCommandValidator : AbstractValidator<CreateRegisterCommand>
    {
        public CreateRegisterCommandValidator()
        {

            RuleFor(x => x.UserDto).NotNull().WithMessage("User data must be provided.");
                RuleFor(x => x.UserDto.UserName)
                    .NotEmpty().WithMessage("Username is required.")
                    .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");
            RuleFor(x => x.UserDto.LastName)
                    .NotEmpty().WithMessage("First name is required.")
                    .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.UserDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.UserDto.PhoneNumber)
            .NotEmpty()
            .Matches(@"^01[0125][0-9]{8}$")
            .WithMessage("Invalid Egyptian phone number.");

            //RuleFor(x => x.UserDto.Password)
            //    .NotEmpty()
            //    .MinimumLength(8)
            //    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            //    .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            //    .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            //    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character."); ==> the validation in Identity will chick it

            RuleFor(x => x.RegistrationToken)
                .NotEmpty();
        }
    }
}
