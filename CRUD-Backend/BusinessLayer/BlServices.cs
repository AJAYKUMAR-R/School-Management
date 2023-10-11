using BusinessLayer.BL_layer;
using BusinessLayer.RespositoryLayer;
using CRUD.BL_layer;
using CRUD.RespositoryLayer;
using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DatabaseLogic;
using DatabaseLayer.DatabseAbstraction;
using DatabaseLayer.DTO;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class BlServices
    {
        public static IServiceCollection RegisterBlServices(this IServiceCollection services)
        {

            //Adding DI for BL layer
            services.AddTransient<IBlSqlLogic, BlSqlLogic>();

            //Adding DI for BL layer  --pagination
            services.AddTransient<IBLPagination, BLPagination>();

            //injecting Auth services
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            //injecting Auth services
            services.AddTransient<IBLFee, BlFee>();


            return services;
        }
    }
}
