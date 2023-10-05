using DatabaseLayer.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudValidation.LoginValidation
{
    public class UserValidation: AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(user => user.Email).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress();
            RuleFor(user => user.Password).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }
    }
}
