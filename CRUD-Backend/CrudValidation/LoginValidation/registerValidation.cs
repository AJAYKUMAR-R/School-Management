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
            RuleFor(customer => customer.StudentName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
            RuleFor(customer => customer.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                ;
            RuleFor(customer => customer.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+|~=`{}\[\]:;""'<>,.?/])(?!.*\s).{8,}$").
                Equal(customer => customer.ConfirmPassword);
            RuleFor(customer => customer.ConfirmPassword)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+|~=`{}\[\]:;""'<>,.?/])(?!.*\s).{8,}$");
            RuleFor(customer => customer.Country)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
            RuleFor(customer => customer.Pincode)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
            RuleFor(customer => customer.Roles)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }
    }
}
