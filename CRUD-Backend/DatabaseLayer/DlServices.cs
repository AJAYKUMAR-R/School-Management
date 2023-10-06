using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DatabaseLogic;
using DatabaseLayer.DatabseAbstraction;
using DatabaseLayer.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public static class DlServices
    {
        public static IServiceCollection RegisterDlServices(this IServiceCollection services)
        {
            //Adding DI for DL layer
            services.AddTransient<IDLSlqLogic, DLSlqLogic>();

            //Adding DI for DL layer --pagination
            services.AddTransient<IDLPagination, DLPagination>();


            services.AddTransient<IDLLogin, DLLogin>();

            return services;
        }
    }
}
