using CrudValidation.LoginValidation;
using DatabaseLayer.DTO;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudValidation
{
    public static class ValidationServices
    {
        public static IServiceCollection RegisterValidationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Register>, RegisterValidation>();

            services.AddScoped<IValidator<User>, UserValidation>();

            return services;
        }
    }
}
