﻿using FluentValidation;
using ShopOnline.WebAdmin.Models;

namespace ShopOnline.WebAdmin.FluentValidations
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password is at least 6 characters");
        }
    }
}