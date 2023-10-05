using DatabaseLayer.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudValidation.LoginValidation
{
    public class RegisterValidation : AbstractValidator<Register>
    {
        public RegisterValidation() 
        {
            RuleFor(user => user.StudentName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
            RuleFor(user => user.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                ;
            RuleFor(user => user.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+|~=`{}\[\]:;""'<>,.?/])(?!.*\s).{8,}$").
                Equal(user => user.ConfirmPassword);
            RuleFor(user => user.ConfirmPassword)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+|~=`{}\[\]:;""'<>,.?/])(?!.*\s).{8,}$");
            RuleFor(user => user.Country)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
            RuleFor(user => user.Pincode)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
            RuleFor(user => user.Roles)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }
    }
}
